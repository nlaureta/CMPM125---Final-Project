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
    [SerializeField] private Vector3 knockback;

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
            //knockback = new Vector3(75f, 10f, 0f);
            enemyHealth.value = HealthManager.Instance.ChangeHealth(enemyIndex, dmgDealt, recovery, knockback);

        }
  
    }
}
