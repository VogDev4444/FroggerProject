using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileKnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public float attackSpeed = 0.5f;
    public float cooldown;
    public float projectileSpeed = 500;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            //way to reduce score add here
        }
    }

}
