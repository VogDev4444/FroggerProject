using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //[Header("Input Action Asset")]
    [SerializeField] public InputActionAsset playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string dodge = "Dodge";
    [SerializeField] private string attack = "Attack";
    [SerializeField] private string look = "Look";

    private InputAction moveAction;
    private InputAction dodgeAction;
    private InputAction attackAction;
    private InputAction lookAction;


     Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float lookSpeed = 10f;


    public Vector2 moveInput { get; private set; }

    public Vector2 lookInput { get; private set; }
    public bool dodgeTrigger { get;private set; }
    public bool attackTrigger { get; private set; }

    //public static Movement Instance { get; private set; }

    public bool invincible;

    //Animator controller
    private Animator anim;

    //where the projectile will be aimed at
    public GameObject reticalRB;
    //the projectile prefab itself
    public Rigidbody2D projectPrefab;
    public Vector3 fireStartPos; //where the projectile spawns
    Vector2 projectileSpeed;

    private void Awake()
    {
        playerControls = this.GetComponent<PlayerInput>().actions;

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        dodgeAction = playerControls.FindActionMap(actionMapName).FindAction(dodge);
        attackAction = playerControls.FindActionMap(actionMapName).FindAction(attack);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        RegisterInputActions();
    }

    //Registering the actions
    void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>(); //reads the value at the time of the input
        moveAction.canceled += context => moveInput = Vector2.zero; //when no player input, defaults to 0

        dodgeAction.performed += context => playerDodge();
        dodgeAction.canceled += context => dodgeTrigger = false;

        attackAction.performed += context => playerFire();
        attackAction.canceled += context => attackTrigger = false;

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>(); //reads the vector2 value to change the retical location
        lookAction.canceled += context => lookInput = Vector2.zero;
    }
    private void onEnable() 
    {
        moveAction.Enable();
        dodgeAction.Enable(); 
        attackAction.Enable();
        lookAction.Enable();
        
    }

    private void onDisable() 
    {
        moveAction.Disable();
        dodgeAction.Disable();
        attackAction.Disable();
        lookAction.Disable();
    }
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


    }

    private void FixedUpdate()
    {
         rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //changes where the retical is aimed as if it is another players
        //reticalRB.transform.Translate(new Vector2(lookInput.x, lookInput.y));

        //need to translate world pos and not local
        reticalRB.GetComponent<Rigidbody2D>().velocity = new Vector2(lookInput.x * lookSpeed, lookInput.y * lookSpeed);

        //Entire section is changing where the projectiles spawn a certain distance away from the player in a radius around them
        Vector3 v = reticalRB.transform.position - this.transform.position;
        v.Normalize();
        v = v * 1.3f;
        fireStartPos = this.transform.position + v;
        //direction it is firing towards
        projectileSpeed = reticalRB.transform.localPosition;
        projectileSpeed.Normalize();
        projectileSpeed = projectileSpeed * 10;

    }

    public void playerDodge()
    {

        //add something to activate dodge animation
        if (moveInput.x > 0)
        {
            anim.SetBool("isDodging", true);
            anim.SetFloat("speed", 1);
        }
        else if (moveInput.x<0)
        {
            anim.SetBool("isDodging", true);
            anim.SetFloat("speed", -1);
        }
        StartCoroutine(InvincibleTimer(2)); //gives invuln frames
        
    }

    public void playerFire()
    {

        //instantiating the bullet 
        Rigidbody2D project = Instantiate(projectPrefab, new Vector3(fireStartPos.x,fireStartPos.y, fireStartPos.z),this.transform.rotation) as Rigidbody2D;
        project.GetComponent<ProjectileKnockBack>().moveDir = projectileSpeed;
        project.AddForce(projectileSpeed * 100);

    }

    IEnumerator InvincibleTimer(float duration)
    {
        if(invincible == false)
        {
            invincible = true;
            dodgeTrigger = true;
            yield return new WaitForSeconds(duration);
            invincible = false;
            dodgeTrigger = false;
            anim.SetBool("isDodging", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Water"))
        {
            moveSpeed = 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            moveSpeed = 5f;
        }
    }
}
