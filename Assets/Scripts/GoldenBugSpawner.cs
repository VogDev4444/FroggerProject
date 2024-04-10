using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBugSpawner : MonoBehaviour
{
    public GameObject goldenBugPrefab; // Reference to the golden bug prefab to spawn
    public float spawnInterval = 15f; // Time interval between golden bug spawns
    public float spawnAreaWidth = 20f; // Width of the area where golden bugs can spawn
    public float spawnAreaHeight = 20f; // Height of the area where golden bugs can spawn
    public int maxGoldenBugs = 1; // Maximum number of golden bugs allowed on the screen

    [SerializeField] private int currentGoldenBugs = 0; // Current number of golden bugs on the screen

    void Start()
    {
        // Start spawning golden bugs at regular intervals
        StartCoroutine(SpawnGoldenBugs());
    }

    IEnumerator SpawnGoldenBugs()
    {
        while (true)
        {
            if (currentGoldenBugs < maxGoldenBugs)
            {
                // Calculate random spawn position within the spawn area
                Vector3 spawnPosition = new Vector3(
                    Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f),
                    Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f),
                    0f
                );

                // Instantiate a golden bug at the calculated spawn position
                GameObject newGoldenBug = Instantiate(goldenBugPrefab, spawnPosition, Quaternion.identity);

                // Play "Idle" animation on the golden bug
                Animator goldenBugAnimator = newGoldenBug.GetComponent<Animator>();
                if (goldenBugAnimator != null)
                {
                    goldenBugAnimator.Play("Idle");
                }

                // Increment the current golden bug count
                currentGoldenBugs++;
            }

            // Wait for the specified spawn interval before spawning the next golden bug
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Method to decrement the current golden bug count when a golden bug is destroyed
    public void GoldenBugDestroyed()
    {
        currentGoldenBugs--;
    }
}
