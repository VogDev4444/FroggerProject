using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BugManager : MonoBehaviour
{
    public int bugCount;
    public TMP_Text bugText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bugText.text = bugCount.ToString();
    }
}
