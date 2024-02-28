using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public BugManager bm;
    public UI_Script score_UI;
    private Animator anim;

    //booleans for state of the players 
    bool isInWater = false;
    public bool isAirborn = false;
    public bool isStaggered;
    public bool isKnockedBack;
    public bool invincible = false;

    //for the knockback and movement anim sprites
    public Vector2 moveInput;
    public Vector2 direction;
    public string lastIn;

    public int playerNum;

    public bool flyingCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bm = FindAnyObjectByType<BugManager>();
        score_UI = FindAnyObjectByType<UI_Script>();
        if(playerNum == 0)
        {
            playerNum = 2;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //gives players time to move out before they just die 
        if (isInWater && !invincible)
        {
            if (playerNum == 1)
            {
                if (bm.bugCount1 > 0)
                {
                    bm.bugCount1--;
                }
            }
            else
            {
                if (bm.bugCount2 > 0)
                {
                    bm.bugCount2--;
                }
            }
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
            if (playerNum == 1)
            {
                bm.bugCount1++;
                //score_UI.increaseScore(1); does not exist
            }
            else
            {
                bm.bugCount2++;
                //score_UI.increaseScore(2); does not exist
            }
        }
        if (other.gameObject.CompareTag("Death") && !invincible)
        {
            if (playerNum == 1)
            {
                if (bm.bugCount1 > 0)
                {
                    bm.bugCount1--;
                }
            }
            else
            {
                if (bm.bugCount2 > 0)
                {
                    bm.bugCount2--;
                }
            }
            StartCoroutine(InvincibleTimer(1));
        }
        if(other.gameObject.CompareTag("Water") && isAirborn == false)
        {
            isInWater = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Water"))
        {
            isInWater = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Death") && !invincible)
        {
            if (playerNum == 1)
            {
                if (bm.bugCount1 > 0)
                {
                    bm.bugCount1--;
                }
            }
            else
            {
                if (bm.bugCount2 > 0)
                {
                    bm.bugCount2--;
                }
            }
            StartCoroutine(InvincibleTimer(1));
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
        
        isKnockedBack = true;
        yield return new WaitForSecondsRealtime(0.15f);
        isKnockedBack = false;
        
    }
}
