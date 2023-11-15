using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float countdown = 120; // 2 minutes

    [SerializeField] 
    TMP_Text timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown > 0) {
            countdown -= Time.deltaTime;
            timerText.text = System.Math.Round(countdown).ToString();
        }
        if(countdown <= 0) {
            // Timer run out mechanic here
            Debug.Log("Draw");
        }    
    }
}
