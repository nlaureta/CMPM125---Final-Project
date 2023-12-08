using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

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

    //Aduio Components
    public AudioSource audioSource;
    public AudioClip guardClip;
    public AudioClip[] attackClips;
    public int[] odds;
    private int totalOdds;

    private void Start()
    {
        if (attackClips == null)
        {
            Debug.LogError("Audio Variety Not provided with clips");
        }
        if (odds.Length != attackClips.Length)
        {
            Debug.LogWarning("odds length doesn't match clips length, using default");
            odds = new int[attackClips.Length];
            for (int i = 0; i < attackClips.Length; i++)
            {
                odds[i] = 1;
                totalOdds++;
            }
        }
        else
        {
            for (int i = 0; i < attackClips.Length; i++)
            {
                totalOdds += odds[i];
            }
            audioSource = GetComponent<AudioSource>();
        }
    }

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
                if (audioSource.isPlaying == false)
                {
                    audioSource.PlayOneShot(guardClip);
                    Debug.Log("Blocked");
                }
            }
            else
            {
                if (audioSource.isPlaying == false)
                {
                    PlayOneShotAttack();
                }
            }
            enemyHealth.value = HealthManager.Instance.ChangeHealth(enemyIndex, dmgDealt, recovery, knockback);

        }
  
    }

    public void PlayOneShotAttack()
    {
        int randOdds = Random.Range(0, totalOdds);
        int oddsAgaisnt = 0;
        int clipID = 0;
        for (int i = 0; i < attackClips.Length; i++)
        {
            oddsAgaisnt += odds[i];
            if (oddsAgaisnt > randOdds)
            {
                clipID = i;
                break;
            }
        }
        audioSource.PlayOneShot(attackClips[clipID]);
    }
}
