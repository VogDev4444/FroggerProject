using Unity.VisualScripting;
using UnityEngine;

public class FakeLiLyPad : MonoBehaviour
{
    public float speed = 2f;
    public float destroyThreshold = 10f;
    public float respawnPositionX = -10f;
    public float riverStartX = -15f;
    public float riverEndX = 15f;
    public float disappearDelay = 3f;

    private float disappearTimer = 0f;
    private bool isPlayerOnPad = false;

    public GameObject player; //keeps track of whats on it
    public GameObject player2;

    void FixedUpdate()
    {
        MoveLilyPad();

        if (isPlayerOnPad)
        {
            // Count down the timer if a player is on the pad
            disappearTimer -= Time.fixedDeltaTime;
            if (disappearTimer <= 0f)
            {
                // Destroy the lily pad if the timer expires
                // needs to make sure the players are not children of the lilypad anymore before it gets destroyed
                if(player != null || player2 != null)
                {
                    if(player != null)
                    {
                        player.transform.SetParent(null);
                    }
                    if(player2 != null)
                    {
                        player2.transform.SetParent (null);
                    }
                    
                }
                Destroy(gameObject);
            }
        }
    }

    void MoveLilyPad()
    {
        // Move the lily pad to the right along the X-axis
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);

        // Check if the lily pad has gone beyond the destroy threshold
        if (transform.position.x > destroyThreshold)
        {
            // Respawn the lily pad
            RespawnLilyPad();
        }
    }

    void RespawnLilyPad()
    {
        // Reset the position of the lily pad to the respawn position
        transform.position = new Vector3(respawnPositionX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // Reset the position of the lily pad to the start of the river
        if (transform.position.x > riverEndX)
        {
            transform.position = new Vector3(riverStartX, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the disappear timer when a player steps on the lily pad
            isPlayerOnPad = true;
            disappearTimer = disappearDelay;
            if(player == null )
            {
                player = other.gameObject;
            }
            else if (player2 == null)
            {
                player2 = other.gameObject;
            }

            // Move the player with the lily pad by setting it as the parent
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(other == player)
            {
                player = null;
            }
            else if (other == player2)
            {
                player2 = null;
            }
            // Reset the state when the player leaves the lily pad
            isPlayerOnPad = false;

            // Remove the lily pad as the parent of the player
            other.transform.SetParent(null);
        }
    }
}
