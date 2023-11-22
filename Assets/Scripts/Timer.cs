using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float countdown = 90; // 2 minutes

    [SerializeField] 
    TMP_Text timerText;
    private RoundManager rm;
    
    // Start is called before the first frame update
    void Start()
    {
         rm = RoundManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown > 0){
            timer();  
        }
    }

    void timer() {
        //if(!HealthManager.Instance.roundEnded) {
            if(countdown > 0) {
                countdown -= Time.deltaTime;
                timerText.text = System.Math.Round(countdown).ToString();
            }
        //}
        if(countdown <= 0) {
            // Timer run out mechanic here
            HealthManager.Instance.setRoundEndFlag();
            //HealthManager.Instance.FlagPrint();
            //if(HealthManager.Instance.roundEnded){
            rm.RoundEnd(-1);
            //}
        }    
    }

}
