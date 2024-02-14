using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugManager : MonoBehaviour
{
    public int bugCount;
    public Text bugText;
    public float moveSpeed = 2f; // Speed at which bugs move

    private BugSpawner bugSpawner;

    void Start()
    {
        // Find the BugSpawner GameObject in the scene
        bugSpawner = FindObjectOfType<BugSpawner>();

        // Start moving bugs
        StartCoroutine(MoveBugs());
    }

    IEnumerator MoveBugs()
    {
        while (true)
        {
            // Find all bug GameObjects in the scene
            GameObject[] bugs = GameObject.FindGameObjectsWithTag("Bug");

            // Move each bug towards a random position on the screen
            foreach (GameObject bug in bugs)
            {
                Vector3 targetPosition = new Vector3(
                    Random.Range(-10f, 10f), // X coordinate within the screen bounds
                    Random.Range(-5f, 5f),   // Y coordinate within the screen bounds
                    0f
                );

                // Calculate direction towards the target position
                Vector3 moveDirection = (targetPosition - bug.transform.position).normalized;

                // Move the bug towards the target position
                bug.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            }

            // Wait for a short interval before moving the bugs again
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bugText.text = "P1 Score: " + bugCount.ToString();
    }

    // Method to be called when a bug is destroyed
    public void BugDestroyed()
    {
        // Decrement the bug count in the BugSpawner
        bugSpawner.BugDestroyed();
    }
}
