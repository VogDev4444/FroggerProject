using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    float newButtonPress = 0.0f;  //to stop from immediately pressing start upon menu appearing

    public void Start()
    {
        newButtonPress = Time.time;
    }
    public void Back()
    {
        if (Time.time > newButtonPress + 0.5f)
        {
            SceneManager.LoadScene("StartMenu");
        }
    }
}
