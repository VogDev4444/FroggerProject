using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMove : MonoBehaviour
{
    public float movementSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        rb.velocity = new Vector2(speedX, speedY);
        if(speedX != 0)
        {
            anim.speed = speedX;
        }
        else
        {
            anim.speed = speedY;
        }
    }
}
