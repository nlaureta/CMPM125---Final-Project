using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.InputSystem;

public class Combat : MonoBehaviour
{
    Player1Controls gamepadControls;
    public Animator punch;
    [SerializeField] private GameObject punchHB, blockIcon;
    bool punchcd, blocking = false;
    float timeRemaining;

    Vector3 gamepadMove;
    public float moveSpeed = 5f;

    
    void Awake() {
        gamepadControls = new Player1Controls();
        gamepadControls.Player1Gameplay.Punch.performed += ctx => GamepadPunch();
        //gamepadControls.Player1Gameplay.Move.performed += ctx => gamepadMove = ctx.ReadValue<Vector2>();
        //gamepadControls.Player1Gameplay.Move.canceled += ctx => gamepadMove = Vector2.zero;
    }

    void OnEnable() {
        gamepadControls.Player1Gameplay.Enable();
    }

    void OnDisable() {
        gamepadControls.Player1Gameplay.Disable();
    }

    void GamepadPunch() {
        if (!punchcd && !blocking)
        {
            //Debug.Log("punching");
            punchHB.SetActive(true);
            punch.SetTrigger("punching");
            //punch.Play("PlayerPunch", -1, 0f);
            punchcd = true;
            StartCoroutine(Cooldown(.5f));
        }
    }


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
            //Debug.Log("Blocked");
        } else if(!punchcd && gamepadControls.Player1Gameplay.Block.ReadValue<float>() > 0) {
            blocking = true;
            blockIcon.SetActive(true);
            punch.SetBool("blocking", true);
            //Debug.Log("Blocked");
        }
        else
        {
            blocking = false;
            blockIcon.SetActive(false);
            punch.SetBool("blocking", false);
        }

       
        // else if (blocking && gamepadControls.Player1Gameplay.Block.ReadValue<float>() == 0)
        // {
        //     blocking = false;
        //     blockIcon.SetActive(false);
        //     punch.SetBool("blocking", false);
        //     Debug.Log("Blocked ended");
        // }

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
