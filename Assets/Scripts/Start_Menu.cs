using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    public CanvasGroup start;
    public CanvasGroup howToPlay;

    private void Start()
    {
        //display start menu
        start.alpha = 1;
        start.interactable = true;
        start.blocksRaycasts = true;
        howToPlay.alpha = 0;
        howToPlay.interactable = false;
        howToPlay.blocksRaycasts = false;
    }

    public void StartGame()
    {
        //display howToPlay canvas
        howToPlay.alpha = 1;
        howToPlay.interactable = true;
        howToPlay.blocksRaycasts = true;
        start.alpha = 0;
        start.interactable = false;
        start.blocksRaycasts = false;
    }

    public void Ready()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void SettingsScene()
    {
        //load settings scene. We can also change this to whatever we want it to be, such as how to play
    }
}
