using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;


public class UI_Script : MonoBehaviour
{
    //use these to switch canvas in scene
    public CanvasGroup gameCanvas;
    public CanvasGroup howToPlay;

    //text that displays scores
    public TMP_Text p1;  
    public TMP_Text p2;

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

    //players stop moving at endgame
    Rigidbody2D player1;
    Movement movementp1;
    Rigidbody2D player2;
    Movement movementp2;

    bool p1win = false;
    bool p2win = false;

    //score
    int p1_score = 0;
    int p2_score = 0;
    int scoreCap = 5;  //change to score that makes sense


    void Start()
    {
        p1.text = "0";
        p2.text = "0";

        //display howToPlay canvas
        howToPlay.alpha = 1;
        gameCanvas.alpha = 0;
    }

    void Update()
    {
        if (p1win || p2win)
        {
            //stop frogs from moving after game is over
            FindPlayers();
            movementp1.enabled = false;  
            player1.velocity = Vector3.zero;
            if (player2 != null)
            {
                movementp2.enabled = false;  
                player2.velocity = Vector3.zero;
            }

            //change UI
            EndGame();
        }
        else
        {
            DisplayScore();
        }

        //in case of emergency
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToStartMenu();
        }
    }

    void DisplayScore()
    {
        //BugManager updates score text directly
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

    public void ChangeCanvas()
    {
        //Happens when Ready! button on HowToPlay canvas is pushed

        //display main game canvas
        gameCanvas.alpha = 1;
        howToPlay.alpha = 0;

        //don't display endgame ui
        p1NewTitle.SetActive(false);
        p1End.SetActive(false);
        p2NewTitle.SetActive(false);
        p2End.SetActive(false);
        win.SetActive(false);
        RestartButton.SetActive(false);
        EndButton.SetActive(false);
    }

    public void Restart()
    {
        //Happens when try again is pushed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToStartMenu()
    {
        //Happens when Back to Start is pushed
        SceneManager.LoadScene("StartMenu");
    }

    public void EndGame()
    {
        //final score is calculated
        p1FinalScore.text = p1_score.ToString();
        p2FinalScore.text = p2_score.ToString();

        //turn off old score ui
        p1OldTitle.SetActive(false);
        p2OldTitle.SetActive(false);
        p1Before.SetActive(false);
        p2Before.SetActive(false);

        StartCoroutine(EndDisplayTimer());
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

    void FindPlayers()
    {
        GameObject frog1 = GameObject.Find("Player");
        player1 = frog1.GetComponent<Rigidbody2D>();
        movementp1 = frog1.GetComponent<Movement>();

        GameObject frog2 = GameObject.Find("Player2");
        if (player2 != null)
        {
            player2 = frog2.GetComponent<Rigidbody2D>();
            movementp2 = frog2.GetComponent<Movement>();
        }
    }

}
