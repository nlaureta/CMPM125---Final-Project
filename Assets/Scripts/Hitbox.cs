using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Slider enemyHealth;
    [SerializeField] private float dmg = -10f;
    [SerializeField] private float recovery = 1f;
    private Animator enemy;
    private float dmgDealt;
    [SerializeField] private Animator hpAnimator;
    [SerializeField] private string enemyTag;
    [SerializeField] private int enemyIndex;

    void OnTriggerEnter(Collider other)
    {
        enemy = other.gameObject.GetComponentInChildren<Animator>();
        dmgDealt = dmg;
        //Debug.Log("collision detected");
        if (other.tag == enemyTag && !enemy.GetBool("Recovering"))
        {
            hpAnimator.SetTrigger("hit");
            if (enemy.GetBool("Block"))
            {
                dmgDealt = dmgDealt / 10;
            }
            //Debug.Log("enemy detected");
            enemyHealth.value = HealthManager.Instance.ChangeHealth(enemyIndex, dmgDealt, recovery);

        }
    }
}
