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

    private InputAction moveAction;
    private InputAction dodgeAction;
    private InputAction attackAction;


     Rigidbody2D rb;
    public float moveSpeed = 5f;


    public Vector2 moveInput { get; private set; }
    public bool dodgeTrigger { get;private set; }
    public bool attackTrigger { get; private set; }

    public static Movement Instance { get; private set; }

    //Animator controller
    private Animator anim;
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
    }
    private void onEnable() 
    {
        moveAction.Enable();
        dodgeAction.Enable(); 
        attackAction.Enable();
        
    }

    private void onDisable() 
    {
        moveAction.Disable();
        dodgeAction.Disable();
        attackAction.Disable();
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
    }
    
}
