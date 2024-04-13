using UnityEngine;

public class LilyPadMovement : MonoBehaviour
{
    public float speed = 2f; // Adjust this value to control the speed of movement
    public float destroyThreshold = 10f; // Adjust this value to control when the lily pad gets destroyed
    public float riverStartX = -48f; // X-position where the river starts
    public float riverEndX = 15f; // X-position where the river ends

    void FixedUpdate()
    {
        // Move the lily pad to the right along the X-axis
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);

        
    }

    // Check if the lily pad has reached the end of the river
    void Update()
    {
        transform.position = new Vector3(transform.position.x+.01f, transform.position.y, transform.position.z);
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
                }
            }
            else
            {
                // Move the player with the lily pad by setting it as the parent
                other.transform.SetParent(null);
                other.transform.SetParent(this.transform);
                if (other.GetComponent<Movement>().onLily == false)
                {
                    other.GetComponent<Movement>().onLily = true;
                }
            }
        }

        if(other.CompareTag("Destroy"))
        {
            Destroy(this.gameObject);
        }
    }

    // Stop moving the player with the lily pad when they exit the trigger
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belongs to a player
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Transform>().parent == this.transform)
            {
                // Remove the lily pad as the parent of the player
                other.GetComponent<Movement>().onLily = false;
                other.transform.SetParent(null);
            }
        }
    }
}
