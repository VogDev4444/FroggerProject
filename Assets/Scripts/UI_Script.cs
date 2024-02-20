using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UI_Script : MonoBehaviour
{
    public GameObject canvas;
    public Rigidbody2D player1;
    public Movement movementScript;

    public TMP_Text p1;  //text that displays scores
    public TMP_Text p2;

    //public GameObject p1_heart_1;  //no longer necessary but keeping just in case
    //public GameObject p1_heart_2;
    //public GameObject p1_heart_3;
    //public GameObject p2_heart_1;
    //public GameObject p2_heart_2;
    //public GameObject p2_heart_3;

    protected bool p1win = false;
    protected bool p2win = false;

    protected int p1_score = 0;
    protected int p2_score = 0;
    int scoreCap = 3;  //change to score that makes sense


    void Start()
    {
        p1.text = "0";
        p2.text = "0";
    }

    void Update()
    {
        DisplayScore();

        if (p1win || p2win)
        {
            movementScript.enabled = false;  //inputs will no longer move the frog when game is over
            player1.velocity = Vector3.zero;  //freeze frog
            canvas.SendMessage("EndGame");
        }
    }

    void DisplayScore()
    {
        p1_score = int.Parse(p1.text);
        p2_score = int.Parse(p2.text);

        if(p1_score >= scoreCap)
        {
            p1win = true;
        }
        if (p2_score >= scoreCap)
        {
            p2win = true;
        }
    }


    //public void P1Health()
    //{
    //    if (p1_heart_1.activeSelf)
    //    {
    //        p1_heart_1.SetActive(false);
    //    }
    //    else if (p1_heart_2.activeSelf)
    //    {
    //        p1_heart_2.SetActive(false);
    //    }
    //    else if (p1_heart_3.activeSelf)
    //    {
    //        p1_heart_3.SetActive(false);
    //        p1win = true;
    //    }
    //    else { }
    //}

    //public void P2Health()
    //{
    //    if (p2_heart_1.activeSelf)
    //    {
    //        p2_heart_1.SetActive(false);
    //    }
    //    else if (p2_heart_2.activeSelf)
    //    {
    //        p2_heart_2.SetActive(false);
    //    }
    //    else if (p2_heart_3.activeSelf)
    //    {
    //        p2_heart_3.SetActive(false);
    //        p2win = true;
    //    }
    //    else { }
    //}
}
