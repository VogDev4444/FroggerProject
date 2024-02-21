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
    public float force = 500;

    public float lifeSpan = 2f;
    public Vector2 moveDir;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Destroy(this.gameObject, lifeSpan);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(moveDir * force,ForceMode2D.Force);
        }
        Destroy(this.gameObject);
    }


}
