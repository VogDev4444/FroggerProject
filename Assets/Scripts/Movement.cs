using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
     Rigidbody2D rb;
    public float moveSpeed = 5f;
    public InputAction playerControls;

    Vector2 moveDirection = Vector2.zero;

    private void onEnable() 
    {
        playerControls.Enable();
    }

    private void onDisable() 
    {
        playerControls.Disable();
    }
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

   
    void Update()
    {
        moveDirection = playerControls.ReadValue<Vector2>();
    }
    private void FixedUpdate() 
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
    
}
