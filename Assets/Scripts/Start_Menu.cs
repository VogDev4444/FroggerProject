using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    public CanvasGroup start;
    public CanvasGroup howToPlay;

    float newButtonPress = 0.0f; //to stop from immediately pressing start upon menu appearing

    private void Start()
    {
        //display start menu
        start.alpha = 1;
        start.interactable = true;
        start.blocksRaycasts = true;
        howToPlay.alpha = 0;
        howToPlay.interactable = false;
        howToPlay.blocksRaycasts = false;

        newButtonPress = Time.time;
    }

    public void StartGame()
    {
        Debug.Log("StartGame was pressed");
        if (Time.time > newButtonPress + 0.5f)
        {
            //display howToPlay canvas
            howToPlay.alpha = 1;
            howToPlay.interactable = true;
            howToPlay.blocksRaycasts = true;
            start.alpha = 0;
            start.interactable = false;
            start.blocksRaycasts = false;
            //update last time button was pressed
            newButtonPress = Time.time;
        }
    }

    public void Ready()
    {
        if (Time.time > newButtonPress + 0.5f)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsScene()
    {
        if (Time.time > newButtonPress + 0.5f)
        {
            SceneManager.LoadScene("CreditsScene");
        }
    }

    public void SettingsScene()
    {
        if (Time.time > newButtonPress + 0.5f)
        {
            SceneManager.LoadScene("SettingsScene");
        }
    }
}
