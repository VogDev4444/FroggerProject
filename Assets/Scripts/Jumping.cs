using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    //Jump Related Variables
    //check to see if we're on the ground
    bool grounded = true;
    //check to see if we can jump
    bool canJump = true;
    //using Fixed Update this will be 4 seconds (variable / 50 cuz Unity runs at 50fps)
    int jumpDuration = 200;
    //how long before you can jump again
    int jumpCooldown = 400;

    void Update()
    {
        if(Input.GetKey("space") && canJump)
        {
            Jump();
        }
        else
        {
            Grounded();
        }
    }

    void FixedUpdate()
    {
        //check to see if we're on the ground, if not then we start draining the duration of the jump
        if(!grounded)
        {
            jumpDuration--;
            if(jumpDuration == 0)
            {
                canJump = false;
            }
        }
        else
        {
            //recharges the jump duration during the cooldown period, or after a jump
            if(jumpDuration < 200)
            {
                jumpDuration++;
            }
        }

        //counts to 8 to allow the player to jump again
        int counter = 0;
        if(!canJump)
        {
            while(!canJump)
            {
                counter++;
                if(counter == jumpCooldown)
                {
                    canJump = true;
                }
            }
        }
    }

    void Jump()
    {
        grounded = false;
        if (gameObject.tag != "PlayerA")
        {
            gameObject.tag = "PlayerA";
            //swaps sprite
        }
    }

    void Grounded()
    {
        grounded = true;
        if(gameObject.tag != "Player")
        {
            gameObject.tag = "Player";
            //swaps sprite
        }
    }
}
