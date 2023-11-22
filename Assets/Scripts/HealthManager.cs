using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int player1Health;
    public int player2Health;

    public bool roundEnded = false;

    [System.Serializable]
    public class Player 
    {
        public GameObject player;
        public int maxHealth;
        public int currHealth;
    }
    public List<Player> players;

    private List<GameObject> _playerHealthPool = new List<GameObject>();

    private static HealthManager _instance;
    public static HealthManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    void Start() {
        
    }

    void Update() {
        if(!roundEnded)
            CheckRoundEnd();
    
    }

    public float ChangeHealth(int playerNum, int amount)
    {
        players[playerNum].currHealth += amount;
        return ((float)players[playerNum].currHealth / (float)players[playerNum].maxHealth);
    }

    void CheckRoundEnd(){
        player1Health = players[0].currHealth;
        player2Health = players[1].currHealth;

        if (player1Health <= 0 || player2Health <= 0){
            RoundManager.Instance.RoundEnd(player1Health <= 0 ? 1 : 0); //pass winning player index
            roundEnded = true;
        }
    }

    public void setRoundEndFlag(){
        roundEnded = true;
        Debug.Log("called");
    }

    public void ResetRoundEndFlag(){
        roundEnded = false;
    }

    public void FlagPrint() {
        Debug.Log(roundEnded);
    }
}
