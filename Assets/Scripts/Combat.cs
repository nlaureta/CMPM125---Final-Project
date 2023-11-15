using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator punch;
    [SerializeField] private GameObject punchHB;
    bool punchcd = false;

    // Update is called once per frame
    void Update()
    {
        if (!punchcd && Input.GetKeyDown(KeyCode.U))
        {
            //Debug.Log("punching");
            punchHB.SetActive(true);
            punch.Play("PlayerPunch", -1, 0f);
            punchcd = true;
            StartCoroutine(Cooldown(1));
        }
    }

    IEnumerator Cooldown(int cdtime)
    {
        yield return new WaitForSeconds(cdtime);
        punchcd = false;
        punchHB.SetActive(false);
    }
}
