using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BugMovement : MonoBehaviour
{
    public float maxDistance = 10f;
    public float minDistance = 1f;
    public float newTargetDelay = 3f;
    public float movementSpeed = 5f; // New movement speed variable

    private Rigidbody2D[] bugRigidbodies;
    private NavMeshAgent[] bugs;

    void Start()
    {
        bugs = GameObject.FindObjectsOfType<NavMeshAgent>();
        bugRigidbodies = new Rigidbody2D[bugs.Length];

        // Lock rotation for each bug
        for (int i = 0; i < bugs.Length; i++)
        {
            bugRigidbodies[i] = bugs[i].GetComponent<Rigidbody2D>();
            if (bugRigidbodies[i] != null)
            {
                bugRigidbodies[i].freezeRotation = true;
            }
        }

        StartCoroutine(MoveBugs());
    }

    IEnumerator MoveBugs()
    {
        while (true)
        {
            foreach (NavMeshAgent bug in bugs)
            {
                if (bug != null && (!bug.hasPath || bug.remainingDistance < minDistance))
                {
                    Vector3 newTarget = GetNewTarget(bug.transform.position);
                    bug.SetDestination(newTarget);
                }
            }
            yield return new WaitForSeconds(newTargetDelay);
        }
    }

    void LateUpdate()
    {
        // Constrain the rotation along the z-axis to prevent flipping
        foreach (NavMeshAgent bug in bugs)
        {
            if (bug != null)
            {
                bug.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    Vector3 GetNewTarget(Vector3 currentPosition)
    {
        Vector3 randomDirection = Random.insideUnitSphere * Random.Range(minDistance, maxDistance);
        randomDirection += currentPosition;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, maxDistance, 1);
        return hit.position;
    }
}
