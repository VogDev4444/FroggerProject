using UnityEngine;

public class LilyPadSpawner : MonoBehaviour
{
    public GameObject lilyPadPrefab; // Reference to the LilyPad prefab
    public float spawnInterval = 5f; // Interval between each lily pad spawn
    public float spawnPositionY = 2f; // Y-position where lily pads should spawn
    public float riverStartX = -15f; // X-position where the river starts
    public float riverEndX = 15f; // X-position where the river ends

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
        // Calculate a random X-position within the river bounds
        float randomX = Random.Range(riverStartX, riverEndX);

        // Spawn a lily pad at the random position
        Instantiate(lilyPadPrefab, new Vector3(randomX, spawnPositionY, 0f), Quaternion.identity);
    }
}