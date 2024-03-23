using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BorderTeleport : MonoBehaviour
{
    public float yValueLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = new Vector3(collision.transform.position.x,yValueLocation,collision.transform.position.z);
    }
}
