using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float countdown = 120; // 2 minutes

    [SerializeField] 
    TMP_Text timerText;

    private bool gameOver;


    private RoundManager rm;
    
    // Start is called before the first frame update
    void Start()
    {
        rm = new RoundManager();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver) {
            if(countdown > 0) {
                countdown -= Time.deltaTime;
                timerText.text = System.Math.Round(countdown).ToString();
            }
        }
        if(countdown <= 0) {
            // Timer run out mechanic here
            rm.RoundEnd(-1);
        }    
    }

}
