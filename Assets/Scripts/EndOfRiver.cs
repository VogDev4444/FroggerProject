using UnityEngine;

public class EndOfRiver : MonoBehaviour
{
    // Called when another collider enters the trigger attached to this object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to a lily pad
        if (other.CompareTag("Ground"))
        {
            // Destroy the lily pad
            Destroy(other.gameObject);
        }
    }
}
