using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject bugPrefab; // Reference to the bug prefab to spawn
    public float spawnInterval = 3f; // Time interval between bug spawns
    public float spawnAreaWidth = 10f; // Width of the area where bugs can spawn
    public float spawnAreaHeight = 4f; // Height of the area where bugs can spawn
    public int maxBugs = 10; // Maximum number of bugs allowed on the screen

    private int currentBugs = 0; // Current number of bugs on the screen

    public UI_Script uiScript; // Reference to the UI_Script

    private List<GameObject> activeBugs = new List<GameObject>(); // List to keep track of active bugs

    void Start()
    {
        // Start spawning bugs at regular intervals
        StartCoroutine(SpawnBugs());
    }

    IEnumerator SpawnBugs()
    {
        while (true)
        {
            // Check if both players' scores are above a threshold to determine if spawning should continue
            if (uiScript.p1_score > 6 && uiScript.p2_score > 6)
            {
                // Destroy any active bugs
                foreach (GameObject bug in activeBugs)
                {
                    Destroy(bug);
                }
                activeBugs.Clear();

                yield break; // Exit the coroutine if both players' scores are above 6
            }
            else
            {
                if (currentBugs < maxBugs)
                {
                    // Calculate random spawn position within the spawn area
                    Vector3 spawnPosition = new Vector3(
                        Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f),
                        Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f),
                        0f
                    );

                    // Instantiate a bug at the calculated spawn position
                    GameObject newBug = Instantiate(bugPrefab, spawnPosition, Quaternion.identity);
                    activeBugs.Add(newBug); // Add the bug to the list of active bugs

                    // Play "Idle" animation on the bug
                    Animator bugAnimator = newBug.GetComponent<Animator>();
                    if (bugAnimator != null)
                    {
                        bugAnimator.Play("Idle");
                    }

                    // Increment the current bug count
                    currentBugs++;
                }

                // Wait for the specified spawn interval before spawning the next bug
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    // Method to decrement the current bug count when a bug is destroyed
    public void BugDestroyed()
    {
        currentBugs--;
    }
}
