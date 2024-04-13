using UnityEngine;

public class LilyPadSpawner : MonoBehaviour
{
    public GameObject lilyPadPrefab; // Reference to the LilyPad prefab
    public float spawnInterval = 5f; // Interval between each lily pad spawn
    public float riverStartX = -15f; // X-position where the river starts
    public float riverEndX = 15f; // X-position where the river ends
    public float lowerSpawnPositionY = 0f; // Lower Y-position where lily pads should spawn
    public float upperSpawnPositionY = 5f; // Upper Y-position where lily pads should spawn

    private float timer; // Timer to track spawn intervals

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a lily pad
        if (timer >= spawnInterval)
        {
            // Spawn a lily pad
            SpawnLilyPad();

            // Reset the timer
            timer = 0f;
        }
    }

    void SpawnLilyPad()
    {
        // Attempt to spawn a lily pad multiple times until a non-overlapping position is found
        const int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            // Calculate a random X-position within the river bounds
            float randomX = Random.Range(riverStartX, riverEndX);

            // Calculate a random Y-position within the specified range
            float randomY = Random.Range(lowerSpawnPositionY, upperSpawnPositionY);

            // Check if the spawn position overlaps with any existing lily pads
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(randomX, randomY), 1f);
            bool overlapping = false;
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Ground"))
                {
                    overlapping = true;
                    break;
                }
            }

            // If the spawn position does not overlap, spawn the lily pad and exit the loop
            if (!overlapping)
            {
                Instantiate(lilyPadPrefab, new Vector3(randomX, randomY, 0f), Quaternion.identity);
                break;
            }
        }
    }
}