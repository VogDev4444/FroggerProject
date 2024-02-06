using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UI_Script : MonoBehaviour
{
    //need to get score from score script (getter methods for both p1 score and p2 score in that script)

    public TMP_Text p1;
    public TMP_Text p2;

    public GameObject p1_heart_1;
    public GameObject p1_heart_2;
    public GameObject p1_heart_3;
    public GameObject p2_heart_1;
    public GameObject p2_heart_2;
    public GameObject p2_heart_3;

    void Start()
    {
        p1.text = "000000";
        p2.text = "000000";
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    P1Health();
        //}

        //p1.text = (get player 1's health from other script) .ToString();
        //p2.text = (get player 2's health from other script) .ToString();
    }

    void P1Health()
    {
        if (p1_heart_3.activeSelf)
        {
            p1_heart_3.SetActive(false);
        }
        else if (p1_heart_2.activeSelf)
        {
            p1_heart_2.SetActive(false);
        }
        else if (p1_heart_1.activeSelf)
        {
            p1_heart_1.SetActive(false);
            //Debug.Log("P1 loses");
        }
        else
        {
            //not sure yet
        }
    }

    void P2Health()
    {
        if (p2_heart_3.activeSelf)
        {
            p2_heart_3.SetActive(false);
        }
        else if (p2_heart_2.activeSelf)
        {
            p2_heart_2.SetActive(false);
        }
        else if (p2_heart_1.activeSelf)
        {
            p2_heart_1.SetActive(false);
            //Debug.Log("P2 loses");
        }
        else
        {
            //not sure yet
        }
    }
    //if player 1 collides with an object, p1 heart sprite disappears
    //if player 2 collides with an object, p2 heart sprite disappears
    //P1Health() and P2Health() will have to be used in other scripts
}
