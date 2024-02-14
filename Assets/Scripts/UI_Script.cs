using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class UI_Script : MonoBehaviour
{
    public GameObject canvas;

    public TMP_Text p1;
    public TMP_Text p2;

    public GameObject p1_heart_1;
    public GameObject p1_heart_2;
    public GameObject p1_heart_3;
    public GameObject p2_heart_1;
    public GameObject p2_heart_2;
    public GameObject p2_heart_3;

    bool p1dead = false;
    //bool p2dead = false;     //will add back in when p2 exists
    //bool gamePaused = false; //still trying to figure out how to pause the game without pausing the menu

    void Start()
    {
        p1.text = "0";
        p2.text = "0";
    }

    void Update()
    {
        if (p1dead) //&& p2dead
        {
            //gamePaused = true;
            canvas.SendMessage("EndGame");
        }
    }

    public void P1Health()
    {
        if (p1_heart_1.activeSelf)
        {
            p1_heart_1.SetActive(false);
        }
        else if (p1_heart_2.activeSelf)
        {
            p1_heart_2.SetActive(false);
        }
        else if (p1_heart_3.activeSelf)
        {
            p1_heart_3.SetActive(false);
            p1dead = true;
        }
        else { }
    }

    public void P2Health()
    {
        if (p2_heart_1.activeSelf)
        {
            p2_heart_1.SetActive(false);
        }
        else if (p2_heart_2.activeSelf)
        {
            p2_heart_2.SetActive(false);
        }
        else if (p2_heart_3.activeSelf)
        {
            p2_heart_3.SetActive(false);
            //p2dead = true;
        }
        else { }
    }
    //if player 1 collides with an object, p1 heart sprite disappears
    //if player 2 collides with an object, p2 heart sprite disappears
}
