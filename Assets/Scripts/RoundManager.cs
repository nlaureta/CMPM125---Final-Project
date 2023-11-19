using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public int roundsToWin = 3;
    private int player1Wins = 0;
    private int player2Wins = 0;

    private static RoundManager _instance;
    public static RoundManager Instance { get { return _instance; } }

    void Awake() {
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
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RoundEnd(int winningPlayerIndex)
    {
        if (winningPlayerIndex != -1) { // round timer not 0
            Debug.Log("Player " + (winningPlayerIndex + 1) + " wins the round!");
            if (winningPlayerIndex == 0) {
                player1Wins++;
            }
            else {
                player2Wins++;
            }

            if (player1Wins >= roundsToWin || player2Wins >= roundsToWin) { //determines who won best of 3
               
                Debug.Log("Game Over! Player " + (player1Wins >= roundsToWin ? 1 : 2) + " wins the game!");
            }
            else{
              
                StartNewRound();
            }
        }else{
            if(HealthManager.Instance.player1Health < HealthManager.Instance.player2Health) {
                Debug.Log("Time's up! Player2 has won");
                StartNewRound();
            }else if(HealthManager.Instance.player1Health > HealthManager.Instance.player2Health) {
                Debug.Log("Time's up! Player1 has won");
                StartNewRound();
            }else {
                Debug.Log("Round tied! Resuming with a new round.");
                StartNewRound();
            }
        }
    }

    void StartNewRound(){
       //reset everything here
       HealthManager.Instance.ResetRoundEndFlag();
    }
}
