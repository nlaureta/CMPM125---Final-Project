using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoundManager : MonoBehaviour
{
    public int roundsToWin = 3;
    private int player1Wins = 0;
    private int player2Wins = 0;

    public Toggle[] player1Toggles;
    public Toggle[] player2Toggles;
    public GameObject player1WinText;
    public GameObject player2WinText;
    public GameObject drawText;
    public Timer time;
    private Transform playerTransform;
    private static RoundManager _instance;
    public static RoundManager Instance { get { return _instance; } }


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player 1").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator StartNewRoundDelay()
    {
        yield return new WaitForSeconds(3); // wait for 3 seconds
        StartNewRound();
    }

    public void RoundEnd(int winningPlayerIndex)
    {
        if (winningPlayerIndex != -1)
        { // round timer not 0
            Debug.Log("Player " + (winningPlayerIndex + 1) + " wins the round!");
            if (winningPlayerIndex == 0)
            {
                //Debug.Log(player1Wins);
                if (player1Wins < 2)
                    player1Toggles[player1Wins].isOn = true;
                player1WinText.SetActive(true);
                player1Wins++;
            }
            else
            {
                if (player2Wins < 2)
                    player2Toggles[player2Wins].isOn = true;
                player2WinText.SetActive(true);
                player2Wins++;
            }

        }
        else
        {
            if (HealthManager.Instance.player1Health < HealthManager.Instance.player2Health)
            {
                Debug.Log("Time's up! Player2 has won");
                player2WinText.SetActive(true);
                player2Wins++;
                //StartNewRound();
                StartCoroutine(StartNewRoundDelay());
            }
            else if (HealthManager.Instance.player1Health > HealthManager.Instance.player2Health)
            {
                Debug.Log("Time's up! Player1 has won");
                player1WinText.SetActive(true);
                player1Wins++;
                //StartNewRound();
                StartCoroutine(StartNewRoundDelay());
            }
            else
            {
                Debug.Log("Round tied! Resuming with a new round.");
                //StartNewRound();
                drawText.SetActive(true);
                StartCoroutine(StartNewRoundDelay());
            }
            //StartCoroutine(StartNewRound());
        }
        //HealthManager.Instance.FlagPrint();
        if (player1Wins >= roundsToWin || player2Wins >= roundsToWin)
        { //determines who won best of 3

            Debug.Log("Game Over! Player " + (player1Wins >= roundsToWin ? 1 : 2) + " wins the game!");
            // Game Over UI here


        }
        else
        {
            //StartNewRound();
            //Debug.Log("Test");
            StartCoroutine(StartNewRoundDelay());
        }

    }

    void StartNewRound()
    {
        //reset everything here
        //resets timer
        Debug.Log("Resetting...");
        time.countdown = 90;
        //turn off text
        player1WinText.SetActive(false);
        player2WinText.SetActive(false);
        drawText.SetActive(false);
        //reset position
        GameObject.FindGameObjectWithTag("Player 1").transform.position = new Vector3(-6.59000015f,-1.59000003f,0.0179928206f);
        GameObject.FindGameObjectWithTag("Enemy").transform.position = new Vector3(6.07310009f, -1.95000005f, 0.0179928206f);
        GameObject.FindGameObjectWithTag("Player 1").transform.localRotation = new Quaternion(0, 0, 0, 0);
        GameObject.FindGameObjectWithTag("Enemy").transform.localRotation = new Quaternion(0, 0, 0, 0);
        //reset healts
        HealthManager.Instance.players[0].currHealth = 100;
        HealthManager.Instance.players[1].currHealth = 100;
        //resets boolean
        HealthManager.Instance.ResetRoundEndFlag();
    }

}
