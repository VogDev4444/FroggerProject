using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsScene()
    {
        //load credits scene
    }

    public void SettingsScene()
    {
        //load settings scene. We can also change this to whatever we want it to be, such as how to play
    }
}
