using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

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


    public Vector2 moveInput { get; private set; }

    public Vector2 lookInput { get; private set; }
    public bool dodgeTrigger { get;private set; }
    public bool attackTrigger { get; private set; }

    public static Movement Instance { get; private set; }

    //Animator controller
    private Animator anim;

    //Retical
    public Rigidbody2D reticalRB;
    public Transform reticalT;
    public GameObject centerObject;    //frog
    Vector3 centerObjectPos;           
    private float radiusOffset = 1f;   //how far away retical is from frog

    private void Awake()
    {
        //makes sure this is the only one to exist for player1
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

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

        dodgeAction.performed += context => dodgeTrigger = true;
        dodgeAction.canceled += context => dodgeTrigger = false;

        attackAction.performed += context => attackTrigger = true;
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
    
    private void playerAttackMethod()
    {
        //spawns the tongue collider or makes it active 

        //knocks the player back from the tongue collider
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
            anim.SetFloat("speed", 1);
        }
        else
        {
            anim.SetFloat("speed", 0);
        }


        //Retical moves around frog in a circle, following mouse.

        //reticalRB.velocity = new Vector2(lookInput.x * speed, lookInput.y * speed); -> unused for now

        centerObjectPos = centerObject.transform.position;

        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //USES OLD INPUT SYSTEM to know where to look, BAD
        
        Vector2 reticalPosition = (mouseScreenPosition - (Vector2)centerObjectPos).normalized * radiusOffset;   
        reticalT.position = (Vector2)centerObjectPos + reticalPosition; 

        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;
        reticalT.up = direction;   

    }

}
