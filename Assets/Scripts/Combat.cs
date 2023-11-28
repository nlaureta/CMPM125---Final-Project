using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator punch;
    [SerializeField] private GameObject punchHB, blockIcon;
    bool punchcd, blocking = false;
    float timeRemaining;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!punchcd && !blocking && Input.GetKey(KeyCode.U))
        {
            //Debug.Log("punching");
            punchHB.SetActive(true);
            punch.SetTrigger("punching");
            //punch.Play("PlayerPunch", -1, 0f);
            punchcd = true;
            StartCoroutine(Cooldown(.5f));
        }

        if (!punchcd && Input.GetKey(KeyCode.I))
        {
            blocking = true;
            blockIcon.SetActive(true);
            punch.SetBool("blocking", true);
        }
        else
        {
            blocking = false;
            blockIcon.SetActive(false);
            punch.SetBool("blocking", false);
        }
    }

    IEnumerator Cooldown(float cdtime)
    {
        timeRemaining = 0;
        while (timeRemaining < cdtime)
        {
            //Debug.Log(timeRemaining);
            timeRemaining += Time.deltaTime;
            yield return null;
        }
        punchcd = false;
        punchHB.SetActive(false);
    }
}
