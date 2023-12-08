using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public float player1Health;
    public float player2Health;

    private float lastHealthValue;
    private Quaternion knockedBackRotation;
    //private bool takingDmg = false;
    private float timeRemaining;
    private Coroutine dmg;

    public bool roundEnded = false;

    [System.Serializable]
    public class Player 
    {
        public GameObject player;
        public Rigidbody playerBody;
        public float maxHealth;
        public float currHealth;
        public Animator enemy;
        public bool takingDmg = false;
        public float yRot;
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

    public float ChangeHealth(int playerNum, float amount, float recoveryTime, Vector3 knockback)
    {
        players[playerNum].currHealth += amount;
        players[playerNum].takingDmg = true;
        players[playerNum].enemy.SetBool("Hit", true);
        players[playerNum].enemy.SetBool("Knocked", true);
        if (players[playerNum].player.transform.rotation.y >= 0)
        {
            knockback = new Vector3(-knockback.x, knockback.y, knockback.z);
        }
        Debug.Log(players[playerNum].player.transform.rotation.y);
        if (!players[playerNum].enemy.GetBool("Block"))
        {
            players[playerNum].playerBody.AddForce(knockback, ForceMode.Impulse);

            //players[playerNum].player.transform.rotation = new Quaternion(0f, players[playerNum].player.transform.rotation.y, -60f, 0f);
            //Debug.Log(players[playerNum].player.transform.rotation);
            players[playerNum].player.transform.rotation *= Quaternion.Euler(0f, 0f, 45f);
        }
        StopCoroutine(Recover(playerNum, recoveryTime));
        dmg = StartCoroutine(Recover(playerNum, recoveryTime));
        return (players[playerNum].currHealth / players[playerNum].maxHealth);
    }

    public IEnumerator Recover(int playerNum, float recoveryTime)
    {
        lastHealthValue = players[playerNum].currHealth;
        while (players[playerNum].takingDmg)
        {
            players[playerNum].takingDmg = false;
            yield return new WaitForSeconds(recoveryTime);
            if (lastHealthValue != players[playerNum].currHealth)
            {
                //lastHealthValue = players[playerNum].currHealth;
                //yield return new WaitForSeconds(1);
                if (players[playerNum].takingDmg)
                {
                    yield break;
                }
                else
                {
                    lastHealthValue = players[playerNum].currHealth;
                    players[playerNum].takingDmg = true;
                }
            } //else
            //{
            //    players[playerNum].takingDmg = false;
            //}
        }
        players[playerNum].enemy.SetBool("Hit", false);
        players[playerNum].enemy.SetBool("Recovering", true);
        //yield return new WaitForSeconds(1);
        timeRemaining = .5f;
        while (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        players[playerNum].enemy.SetBool("Recovering", false);
        players[playerNum].enemy.SetBool("Knocked", false);
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
