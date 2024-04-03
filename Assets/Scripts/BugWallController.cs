using UnityEngine;

public class BugWallController : MonoBehaviour
{
    public Transform centerPoint; // Center point where bugs will return

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bug"))
        {
            // Calculate direction from bug to center point
            Vector3 directionToCenter = centerPoint.position - collision.transform.position;

            // Normalize direction
            directionToCenter.Normalize();

            // Reverse bug's velocity to bounce it off the wall
            Rigidbody2D bugRigidbody = collision.GetComponent<Rigidbody2D>();
            if (bugRigidbody != null)
            {
                bugRigidbody.velocity = directionToCenter * bugRigidbody.velocity.magnitude;
            }
        }
    }
}