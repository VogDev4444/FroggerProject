using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class UI_Script : MonoBehaviour
{
    public PlayerInput playerController;
    public PlayerInput player2Controler;

    //text that displays scores
    public TMP_Text p1;  
    public TMP_Text p2;

    //ui turned off at endgame
    public GameObject p1OldTitle;
    public GameObject p1Before;
    public GameObject p2OldTitle;
    public GameObject p2Before;
    public GameObject separator;

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
    [SerializeField] Rigidbody2D player1;
    [SerializeField] Rigidbody2D player2;

    bool p1win = false;
    bool p2win = false;

    //score
    int p1_score = 0;
    int p2_score = 0;
    public  int scoreCap = 10;  //change to score that makes sense


    void Start()
    {
        Cursor.visible = false; //Makes cursor invisible when the ready button is pushed

        //don't display endgame ui
        p1NewTitle.SetActive(false);
        p1End.SetActive(false);
        p2NewTitle.SetActive(false);
        p2End.SetActive(false);
        win.SetActive(false);
        RestartButton.SetActive(false);
        EndButton.SetActive(false);

        //set score text to 0
        p1.text = "0";
        p2.text = "0";

        FindPlayers();
    }

    void Update()
    {
        if (p1win || p2win)
        {
            //stop frogs from moving after game is over  
            player1.velocity = Vector3.zero;
            if (player2 != null)
            { 
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

    public void Restart()
    {
        if (player1.GetComponentInParent<Transform>() != null)
        {
            // Remove the lily pad as the parent of the player
            player1.GetComponent<Movement>().onLily = false;
            player1.transform.SetParent(null);
        }
        if (player2 != null)
        {
            if (player2.GetComponentInParent<Transform>() != null)
            {
                // Remove the lily pad as the parent of the player
                player2.GetComponent<Movement>().onLily = false;
                player2.transform.SetParent(null);
            }
        }
        //Happens when try again is pushed
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToStartMenu()
    {
        if (player1.GetComponentInParent<Transform>() != null)
        {
            // Remove the lily pad as the parent of the player
            player1.GetComponent<Movement>().onLily = false;
            player1.transform.SetParent(null);
        }
        if (player2 != null) 
        { 
            if (player2.GetComponentInParent<Transform>() != null)
            {
                // Remove the lily pad as the parent of the player
                player2.GetComponent<Movement>().onLily = false;
                player2.transform.SetParent(null);
            }
        }

        //Happens when Back to Start is pushed
        Cursor.visible=true; //makes mouse visible again
        SceneManager.LoadScene("StartMenu");
    }

    public void EndGame()
    {
        //final score is calculated
        Cursor.visible=true;
        p1FinalScore.text = p1_score.ToString();
        p2FinalScore.text = p2_score.ToString();

        //turn off old score ui
        p1OldTitle.SetActive(false);
        p2OldTitle.SetActive(false);
        p1Before.SetActive(false);
        p2Before.SetActive(false);
        separator.SetActive(false);

        playerController.SwitchCurrentActionMap("UI");
        //player2Controler.SwitchCurrentActionMap("UI");

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

        GameObject frog2 = GameObject.Find("Player2");
        if (frog2 != null)
        {
            player2 = frog2.GetComponent<Rigidbody2D>();
        }
    }

}
