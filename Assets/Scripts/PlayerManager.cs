using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public BugManager bm;
    public UI_Script health_UI;
    private Animator anim;
    public Rigidbody2D rigidbody;

    //booleans for state of the players 
    bool isInWater = false;
    public bool isAirborn;
    public bool isStaggered;
    public bool isKnockedBack;
    public bool invincible = false;

    //for the knockback and movement anim sprites
    public Vector2 moveInput;
    public Vector2 direction;
    public string lastIn;

    int health = 3;


    public bool flyingCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //gives players time to move out before they just die 
        if (isInWater && !invincible)
        {
            health -= 1;
            health_UI.P1Health();
            StartCoroutine(InvincibleTimer(5)); //aftrer 5 seconds players take damage again
        }
        
    }

    public void FixedUpdate()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bug")){
            Destroy(other.gameObject);
            bm.bugCount++;
        }
        if (other.gameObject.CompareTag("Death") && !invincible)
        {
            health -= 1;
            health_UI.P1Health();
            StartCoroutine(KnockBack(5, other.transform.position));
            StartCoroutine(InvincibleTimer(2));
        }
        if(other.gameObject.CompareTag("Water"))
        {
            isInWater = true;
            StartCoroutine(KnockBack(10, this.transform.position));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            isInWater = false;
        }
    }

    public void SetMovement(Vector2 input)
    {
        if (input.x != 0 && input.y != 0)
        {
            input.y /= 1.5f;
            input.x /= 1.5f;
            input.x *= 2f;
        }
        moveInput = input;
        //TODO: normalize "direction" to 8 for animation calls
        if (moveInput.x > 0)
        {
            if (moveInput.y > 0)
            {
                anim.Play("WalkingNE");
                lastIn = "NE";
            }
            if (moveInput.y < 0)
            {
                anim.Play("WalkingSE");
                lastIn = "SE";
            }
            else
            {
                anim.Play("WalkingE");
                lastIn = "E";
            }
        }
        if (moveInput.x < 0)
        {
            if (moveInput.y > 0)
            {
                anim.Play("WalkingNW");
                lastIn = "NW";
            }
            if (moveInput.y < 0)
            {
                anim.Play("WalkingSW");
                lastIn = "SW";
            }
            else
            {
                anim.Play("WalkingW");
                lastIn = "W";
            }
        }
        if (moveInput.x == 0)
        {
            if (moveInput.y > 0)
            {
                anim.Play("WalkingN");
                lastIn = "N";
            }
            if (moveInput.y < 0)
            {
                anim.Play("WalkingS");
                lastIn = "S";
            }
        }
        if (moveInput != Vector2.zero)
        {
            direction = input;
        }
        if (moveInput == Vector2.zero)
        {
            anim.Play(lastIn);
        }
    }



    IEnumerator InvincibleTimer(float duration)
    {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }

    public IEnumerator KnockBack(float _knockback, Vector2 origin)
    {
        Vector2 playerPos = transform.position;
        Vector2 dir = (origin - playerPos);
        Debug.Log(origin);
        Debug.Log(playerPos);
        Debug.Log(dir);
        rigidbody.AddForce(-dir * _knockback, ForceMode2D.Force);
        isKnockedBack = true;
        yield return new WaitForSecondsRealtime(0.15f);
        isKnockedBack = false;
        rigidbody.velocity = Vector2.zero;
    }
}
