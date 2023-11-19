using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Slider enemyHealth;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collision detected");
        if (other.tag == "Enemy")
        {
            //Debug.Log("enemy detected");
            enemyHealth.value = HealthManager.Instance.ChangeHealth(1, -10);
    
        }
    }
}
