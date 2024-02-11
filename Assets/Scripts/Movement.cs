using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        rb.velocity = new Vector2(speedX, speedY);
    }
}
