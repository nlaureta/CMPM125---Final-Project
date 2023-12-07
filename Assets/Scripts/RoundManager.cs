using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoundManager : MonoBehaviour
{
    public int roundsToWin = 2;
    private int player1Wins = 0;
    private int player2Wins = 0;
    [SerializeField] private Slider enemyHealthbar;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private GameObject gameOverScreen;

    public Toggle[] player1Toggles;
    public Toggle[] player2Toggles;
    public GameObject player1WinText;
    public GameObject player2WinText;
    public GameObject drawText;
    public Timer time;

    [SerializeField] private GameObject p1, p2;



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

    IEnumerator StartNewRoundDelay(){
        yield return new WaitForSeconds(3); // wait for 3 seconds
        StartNewRound();
    }
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        Debug.Log("player1Wins is now: " + player1Wins);
    }

    // Update is called once per frame
    void Update()
    {

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
                //P1R1.color = Color.green;
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
        if (player1Wins == roundsToWin || player2Wins == roundsToWin)
        { //determines who won best of 3

            Debug.Log("Game Over! Player " + (player1Wins >= roundsToWin ? 1 : 2) + " wins the game!");
            // Game Over UI here
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;

        }
        else
        {
            //StartNewRound();
            //Debug.Log("Test");
            StartCoroutine(StartNewRoundDelay());
            Debug.Log("player1Wins is now: " + player1Wins);
        }

    }

    public void StartNewRound()
    {
        //reset everything here
        //reset timer
        time.countdown = 90;
        //reset text
        player1WinText.SetActive(false);
        player2WinText.SetActive(false);
        drawText.SetActive(false);
        //reset healthbar
        enemyHealthbar.value = 1;
        playerHealthBar.value = 1;
        //reset player positions
        //GameObject.FindGameObjectWithTag("Player 1").transform.position = new Vector3(-4.59989977f, -1.95000005f, 0.0179928206f);
        //GameObject.FindGameObjectWithTag("Enemy").transform.position = new Vector3(6.07310009f, -1.95000005f, 0.0179928206f);
        //GameObject.FindGameObjectWithTag("Player 1").transform.localRotation = new Quaternion(0, 0, 0, 0);
        //GameObject.FindGameObjectWithTag("Enemy").transform.localRotation = new Quaternion(0, 0, 0, 0);
        p1.transform.position = new Vector3(-6f, -1.25f, 0f);
        p2.transform.position = new Vector3(6f, -1.25f, 0f);
        p1.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
        p2.transform.localRotation = new Quaternion(0f, 180f, 0f, 0f);
        //reset player health value
        HealthManager.Instance.players[0].currHealth = 100;
        HealthManager.Instance.players[1].currHealth = 100;
        //reset round boolean
        HealthManager.Instance.ResetRoundEndFlag();
    }

}
