using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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

    private PlayerManager playerManager;


    Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float baseMoveSpeed = 5f;
    public float lookSpeed = 10f;

    //variable to hold the sfx that plays when attacking - Jax
    public AudioSource attackSFX;

    public Vector2 moveInput { get; private set; }

    public Vector2 lookInput { get; private set; }
    public bool dodgeTrigger { get; private set; }
    public bool attackTrigger = false;

    //public static Movement Instance { get; private set; }

    public bool invincible = false;
    private bool canDodge = true;
    private bool isStaggered = false; //plays stagger animation when hit
    public bool inWater = false;
    public bool onLily = false;

    //Animator controller
    private Animator anim;

    //where the projectile will be aimed at
    public GameObject reticalRB;
    //the projectile prefab itself
    public Rigidbody2D projectPrefab;
    public Vector3 fireStartPos; //where the projectile spawns
    Vector2 projectileSpeed;
    public GameObject firePointer;

    private bool kbMouse = false;

    Collider2D bodyCol;

    private void Awake()
    {
        playerControls = this.GetComponent<PlayerInput>().actions;

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        dodgeAction = playerControls.FindActionMap(actionMapName).FindAction(dodge);
        attackAction = playerControls.FindActionMap(actionMapName).FindAction(attack);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        RegisterInputActions();

        Debug.Log(""+lookAction.ToString());
        UnityEngine.Cursor.visible = false;

        if (lookAction.ToString() == "Player/Look[/Mouse/position]")
        {
            kbMouse = true;
        }
        Debug.Log(kbMouse);
    }

    //Registering the actions
    void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>(); //reads the value at the time of the input
        moveAction.canceled += context => moveInput = Vector2.zero; //when no player input, defaults to 0

        dodgeAction.performed += context => playerDodge(); // Changed method name to follow convention
        dodgeAction.canceled += context => dodgeTrigger = false;

        attackAction.performed += context => playerFire();
        //attackAction.canceled += context => attackTrigger = false;

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.performed += context => playerLookMethod();//reads the vector2 value to change the retical location
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
        bodyCol = GetComponent<Collider2D>();

        playerManager = GetComponent<PlayerManager>();


    }

    private void FixedUpdate()
    {
       if(isStaggered == false)
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
        }
        else
        {
            rb.velocity = Vector2.zero;
        }


        //sets the animation when inwater at different times or when the player is on lily
        if (inWater == true && onLily == false)
        {
            anim.SetBool("inWater", true);
        }
        else if (inWater == false && invincible == false)
        {
            anim.SetBool("inWater", false);
            moveSpeed = 5;
        }
       if(inWater == false && invincible == true)
        {
            anim.SetBool("inWater", false);
        }
       else if (inWater == true && onLily == true)
        {
            anim.SetBool("inWater", false);
            moveSpeed = baseMoveSpeed;
        }
       else if (inWater == true && invincible == false && onLily == false)
        {
            anim.SetBool("inWater", true);
            
            moveSpeed = 2f;
            
        }
       
        
        
    }
  
    public void playerLookMethod()
    {
        //changes where the retical is aimed as if it is another players
        //reticalRB.transform.Translate(new Vector2(lookInput.x, lookInput.y));
        if (kbMouse == true)
        {
            reticalRB.GetComponent<Rigidbody2D>().velocity = new Vector2(lookInput.x * lookSpeed, lookInput.y * lookSpeed); //older retical, will disable sprite render

        }
        else
        {
            reticalRB.GetComponent<Rigidbody2D>().velocity = new Vector2(lookInput.x * lookSpeed, lookInput.y * lookSpeed); //older retical, will disable sprite render
        }


        updateRetical();
    }

    public void updateRetical()
    {
        //Entire section is changing where the projectiles spawn a certain distance away from the player in a radius around them
        Vector3 v = reticalRB.transform.position - this.transform.position;
        v.Normalize();
        v = v * 1.4f;
        fireStartPos = this.transform.position + v;
        firePointer.transform.position = fireStartPos; //actual retical
        reticalRB.transform.position = firePointer.transform.position;
        //direction it is firing towards
        projectileSpeed = reticalRB.transform.localPosition;
        projectileSpeed.Normalize();

        //Changes direction of  heart, does not work rn
        float radvalue = Mathf.Atan2(projectileSpeed.y, projectileSpeed.x);
        float angle = (radvalue * (180/ Mathf.PI))+90;
        firePointer.transform.rotation = Quaternion.Euler(0, 0, angle);

        //This changes the spawn location of the projectile 
        projectileSpeed = projectileSpeed * 10;
    }

    public void playerDodge()
    {

        if (canDodge)
        {
            anim.SetBool("isDodging", true); // Set animation parameters for dodge

            StartCoroutine(InvincibleTimer(.4f)); // Start invincibility timer
            StartCoroutine(DodgeCooldown(5)); // Start dodge cooldown timer
        }

    }

    public void playerFire()
    {
        if(attackTrigger == false && isStaggered == false)
        {
            //instantiating the bullet 
            updateRetical();
            Rigidbody2D project = Instantiate(projectPrefab, new Vector3(fireStartPos.x, fireStartPos.y, fireStartPos.z), this.transform.rotation) as Rigidbody2D;
            project.GetComponent<ProjectileKnockBack>().moveDir = projectileSpeed;
            project.AddForce(projectileSpeed * 100);
            attackSFX.Play();
            StartCoroutine(attackCooldown(1));
        }


    }

    IEnumerator InvincibleTimer(float duration)
    {
        invincible = true; // Set invincibility flag to true
        dodgeTrigger = true;
        inWater = false;
        moveSpeed = baseMoveSpeed * 2;
        

        yield return new WaitForSeconds(duration);

        
        moveSpeed = baseMoveSpeed;
        invincible = false; // Reset invincibility flag
        dodgeTrigger = false;

        anim.SetBool("isDodging", false); // Reset animation parameters for dodge
    }

    IEnumerator attackCooldown(float cooldown)
    {
        attackTrigger = true;
        changeRetical(0);
        yield return new WaitForSeconds(cooldown);
        changeRetical(1);
        attackTrigger = false;
    }

    IEnumerator DodgeCooldown(float cooldownDuration)
    {
        canDodge = false; // Set canDodge flag to false during cooldown
        
        yield return new WaitForSeconds(cooldownDuration);
        
        canDodge = true; // Reset canDodge flag after cooldown duration
    }

    IEnumerator staggerEffect(float duration)
    {
        isStaggered = true;
        canDodge = false;
        anim.SetBool("isStaggered", true);
        yield return new WaitForSeconds(duration);
        anim.SetBool("isStaggered", false);
        canDodge = true;
        isStaggered = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !invincible) // Check if the player is not invulnerable
        {
            playerManager.StealPoint();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Water") && !invincible)
        {
            {
                inWater = true;
                
            }
        }
        if (collision.CompareTag("Enemy") && !invincible) // Check if the player is not invulnerable
        {
            playerManager.SubtractScore();
            getStaggered(1f);
        }
        if (collision.CompareTag("Ground") && !invincible) // Check if the player is not invulnerable
        {
            inWater = false;
            if(moveSpeed != baseMoveSpeed)
            {
                moveSpeed = baseMoveSpeed;
            }
        }
        else if (collision.CompareTag("Ground") && invincible) // Check if the player is not invulnerable
        {
            inWater = false;
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //keeps slow even in water after dodging

        if (collision.CompareTag("Water"))
        {
            inWater = true;
            
        }
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            inWater = false;
            moveSpeed = 5f;
            
        }
        
    }

    
    public void getStaggered(float duration)
    {
        StartCoroutine(staggerEffect(duration));
    }

    public void changeRetical(int mode)
    {
        if(mode == 0)
        {
            firePointer.GetComponent<SpriteRenderer>().GetComponent<changeSprite>().changeRetical(0);
        }
        else
        {
            firePointer.GetComponent<SpriteRenderer>().GetComponent<changeSprite>().changeRetical(1);
        }
        
    }
}