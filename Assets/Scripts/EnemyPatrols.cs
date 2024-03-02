using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed at which the enemy moves
    public float changeDirectionInterval = 2f; // Interval to change direction
    public float maxWanderDistance = 5f; // Maximum distance the enemy can wander

    private Vector3 targetPosition; // Current target position for wandering
    private float timer; // Timer to track when to change direction

    void Start()
    {
        // Start by setting a random target position
        targetPosition = GetRandomPosition();
    }

    void Update()
    {
        // Move towards the target position
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
}
