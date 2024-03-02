using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BugManager : MonoBehaviour
{
    public int bugCount1;
    public int bugCount2;
    public TMP_Text bugText1;
    public TMP_Text bugText2;

    private BugSpawner bugSpawner;

    void Start()
    {
        bugSpawner = FindObjectOfType<BugSpawner>();
        StartCoroutine(UpdateBugCounts());
    }

    IEnumerator UpdateBugCounts()
    {
        while (true)
        {
            bugText1.text = bugCount1.ToString();
            bugText2.text = bugCount2.ToString();
            yield return null;
        }
    }

    public void BugDestroyed()
    {
        bugSpawner.BugDestroyed();
    }
}
