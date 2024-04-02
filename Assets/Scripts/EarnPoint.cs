using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EarnPoint : MonoBehaviour
{
    //text appears for 1 second when points are earned or lost. Called from PlayerManager

    public TMP_Text pointchange;  

    string pointUpdate = "";

    void Start()
    {
        pointchange.text = "";
    }

    //if player gets fly, text = "+1" for 1 second
    //if player gets golden fly, text = "+2" (maybe) for 1 second
    //if player gets hit (red appears), text = "-1" for 1 second
    //if player hits other player and steals point, text = "+1" for 1 second

    public void PlusOne()
    {
        pointUpdate = "+1";
        StartCoroutine(DisplayPointChange());
    }

    public void PlusTwo()
    {
        pointUpdate = "+2";
        StartCoroutine(DisplayPointChange());
    }

    public void MinusOne()
    {
        pointUpdate = "-1";
        StartCoroutine(DisplayPointChange());
    }

    public void MinusTwo()
    {
        pointUpdate = "-2";
        StartCoroutine(DisplayPointChange());
    }

    IEnumerator DisplayPointChange()
    {
        pointchange.text = pointUpdate;
        yield return new WaitForSeconds(1);
        pointchange.text = "";
    }
}
