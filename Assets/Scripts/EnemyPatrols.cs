using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    public float landMoveSpeed = 3f; // Speed at which the enemy moves on land
    public float waterMoveSpeed = 1.5f; // Speed at which the enemy moves in water
    public float changeDirectionInterval = 2f; // Interval to change direction
    public float maxWanderDistance = 5f; // Maximum distance the enemy can wander

    private Vector3 targetPosition; // Current target position for wandering
    private float timer; // Timer to track when to change direction
    private bool isInWater = false; // Flag to track if the enemy is in water

    void Start()
    {
        // Start by setting a random target position
        targetPosition = GetRandomPosition();
    }

    void Update()
    {
        // Move towards the target position with appropriate speed
        float moveSpeed = isInWater ? waterMoveSpeed : landMoveSpeed;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the enemy has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Reset the timer
            timer += Time.deltaTime;

            // If it's time to change direction, get a new random target position
            if (timer >= changeDirectionInterval)
            {
                targetPosition = GetRandomPosition();
                timer = 0f;
            }
        }
    }

    // Function to get a random position within the maximum wander distance
    private Vector3 GetRandomPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle * maxWanderDistance;
        Vector3 randomPosition = transform.position + new Vector3(randomDirection.x, randomDirection.y, 0f);
        return randomPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            // Set the flag to indicate that the enemy is in water
            isInWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            // Reset the flag when the enemy exits water
            isInWater = false;
        }
    }
}
