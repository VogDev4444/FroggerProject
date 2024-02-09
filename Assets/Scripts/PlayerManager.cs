using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public BugManager bm;
    public UI_Script health_UI;

    int health = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bug")){
            Destroy(other.gameObject);
            bm.bugCount++;
        }
        if (other.gameObject.CompareTag("Death"))
        {
            health -= 1;
            health_UI.P1Health();
        }
    }
}
