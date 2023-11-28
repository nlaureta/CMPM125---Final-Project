using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Slider enemyHealth;
    [SerializeField] private float dmg = -10f;
    private Animator enemy;
    private float dmgDealt;

    void OnTriggerEnter(Collider other)
    {
        enemy = other.gameObject.GetComponent<Animator>();
        dmgDealt = dmg;
        //Debug.Log("collision detected");
        if (other.tag == "Enemy")
        {
<<<<<<< Updated upstream

=======
            if (enemy.GetCurrentAnimatorStateInfo(0).IsName("Blocking"))
            {
                dmgDealt = dmgDealt / 10;
            }
>>>>>>> Stashed changes
            //Debug.Log("enemy detected");
            enemyHealth.value = HealthManager.Instance.ChangeHealth(1, dmgDealt);
    
        }
    }
}
