using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
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

    public float ChangeHealth(int playerNum, int amount)
    {
        players[playerNum].currHealth += amount;
        return ((float)players[playerNum].currHealth / (float)players[playerNum].maxHealth);
    }
}
