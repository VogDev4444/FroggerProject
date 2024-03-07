using Unity.VisualScripting;
using UnityEngine;

public class LilyPadMovement : MonoBehaviour
{
    public float speed = 2f; // Adjust this value to control the speed of movement
    public float destroyThreshold = 10f; // Adjust this value to control when the lily pad gets destroyed
    public float respawnPositionX = -10f; // X-position where the lily pad respawns
    public float riverStartX = -15f; // X-position where the river starts
    public float riverEndX = 15f; // X-position where the river ends

    void FixedUpdate()
    {
        // Move the lily pad to the right along the X-axis
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);

        // Check if the lily pad has gone beyond the destroy threshold
        if (transform.position.x > destroyThreshold)
        {
            // Respawn the lily pad at the respawn position
            RespawnLilyPad();
        }
    }

    void RespawnLilyPad()
    {
        // Reset the position of the lily pad to the respawn position
        transform.position = new Vector3(respawnPositionX, transform.position.y, transform.position.z);
    }

    // Check if the lily pad has reached the end of the river
    void Update()
    {
        if (transform.position.x > riverEndX)
        {
            // Reset the position of the lily pad to the start of the river
            transform.position = new Vector3(riverStartX, transform.position.y, transform.position.z);
        }
    }

    // Detect collisions with players
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to a player
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Transform>().parent == null)
            {
                // Move the player with the lily pad by setting it as the parent
                other.transform.SetParent(transform);
                if (other.GetComponent<Movement>().onLily == false)
                {
                    other.GetComponent<Movement>().onLily = true;
                };
            }
        }
    }

    // Stop moving the player with the lily pad when they exit the trigger
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belongs to a player
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<Transform>().parent == this.transform)
            {
                //if (parent of player is the lilypad that it's on other.gameObject.parent = lilypad){}
                // Remove the lily pad as the parent of the player
                other.GetComponent<Movement>().onLily = false;
                other.transform.SetParent(null);
            }
            
            
        }
        
    }
}
