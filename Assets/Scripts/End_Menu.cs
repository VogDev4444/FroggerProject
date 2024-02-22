using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class End_Menu : UI_Script
{
    //ui turned off at endgame
    public GameObject p1OldTitle;
    public GameObject p1Before;
    public GameObject p2OldTitle;
    public GameObject p2Before;

    //ui turned on at endgame
    public GameObject p1NewTitle;
    public GameObject p1End;
    public TMP_Text p1FinalScore;
    public GameObject p2NewTitle;
    public GameObject p2End;
    public TMP_Text p2FinalScore;

    public GameObject win;
    public TMP_Text winText;
    public GameObject RestartButton;
    public GameObject EndButton;

    void Start()
    {
        //don't display endgame ui
        p1NewTitle.SetActive(false);
        p1End.SetActive(false);
        p2NewTitle.SetActive(false);
        p2End.SetActive(false);
        win.SetActive(false);
        RestartButton.SetActive(false);
        EndButton.SetActive(false);
    }

    public void EndGame()
    {
        Cursor.visible = true;
        //final score is calculated
        p1_score = int.Parse(p1.text);
        p2_score = int.Parse(p2.text);
        p1FinalScore.text = p1.text;
        p2FinalScore.text = p2.text;

        //turn off old score ui
        p1OldTitle.SetActive(false);
        p2OldTitle.SetActive(false);
        p1Before.SetActive(false);
        p2Before.SetActive(false);

        StartCoroutine(EndDisplayTimer());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //movementScript.enabled = true;
    }

    public void BackToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    IEnumerator EndDisplayTimer()
    {
        //pause the game somehow without pausing this coroutine- Time.timeScale = 0 paused this method too;

        p1NewTitle.SetActive(true);

        yield return new WaitForSeconds(1);

        p1End.SetActive(true);

        yield return new WaitForSeconds(1);

        p2NewTitle.SetActive(true);

        yield return new WaitForSeconds(1);

        p2End.SetActive(true);

        yield return new WaitForSeconds(2);

        win.SetActive(true);

        //determine who won
        if (p1win && p2win)
        {
            winText.text = "DRAW!";
        }
        else if (p1win)
        {
            winText.text = "PLAYER 1 WINS!";
        }
        else
        {
            winText.text = "PLAYER 2 WINS!";
        }

        yield return new WaitForSeconds(1);


        //display buttons
        RestartButton.SetActive(true);
        EndButton.SetActive(true);
    }
}
