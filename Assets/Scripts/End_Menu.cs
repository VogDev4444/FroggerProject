using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class End_Menu : MonoBehaviour
{
    //ui from actual game
    public GameObject p1OldTitle;
    public GameObject p1Before;
    public TMP_Text p1Score;
    public GameObject p2OldTitle;
    public GameObject p2Before;
    public TMP_Text p2Score;

    //ui for endgame
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

    int p1score;
    int p2score;

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
        //final score is calculated
        p1score = int.Parse(p1Score.text);
        p2score = int.Parse(p2Score.text);
        p1FinalScore.text = p1Score.text;
        p2FinalScore.text = p2Score.text;

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
    }

    public void BackToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    IEnumerator EndDisplayTimer()
    {
        //add: blur the background

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
        if (p1score < p2score)
        {
            winText.text = "PLAYER 2 WINS!";
        }
        else if (p1score > p2score)
        {
            winText.text = "PLAYER 1 WINS!";
        }
        else
        {
            winText.text = "DRAW!";
        }

        yield return new WaitForSeconds(1);

        //display buttons
        RestartButton.SetActive(true);
        EndButton.SetActive(true);
    }
}
