using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public float player1Health;
    public float player2Health;

    private float lastHealthValue;
    private bool takingDmg = false;
    private float timeRemaining;

    public bool roundEnded = false;

    [System.Serializable]
    public class Player 
    {
        public GameObject player;
        public float maxHealth;
        public float currHealth;
        public Animator enemy;
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

    public float ChangeHealth(int playerNum, float amount)
    {
        players[playerNum].currHealth += amount;
        takingDmg = true;
        players[playerNum].enemy.SetBool("Hit", true);
        //players[playerNum].player.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
        StartCoroutine(Recover(playerNum));
        return (players[playerNum].currHealth / players[playerNum].maxHealth);
    }

    public IEnumerator Recover(int playerNum)
    {
        lastHealthValue = players[playerNum].currHealth;
        while (takingDmg)
        {
            yield return new WaitForSeconds(1.5f);
            if (lastHealthValue == players[playerNum].currHealth)
            {
                takingDmg = false;
            }
            else
            {
                lastHealthValue = players[playerNum].currHealth;
            }
        }
        players[playerNum].enemy.SetBool("Hit", false);
        players[playerNum].enemy.SetBool("Recovering", true);
        //yield return new WaitForSeconds(1);
        timeRemaining = 1f;
        while (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        players[playerNum].enemy.SetBool("Recovering", false);
        //players[playerNum].enemy.SetTrigger("Recovered");
        //players[playerNum].player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
