using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement mover;
    private Combat combat;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<PlayerMovement>();
        var combaters = FindObjectsOfType<Combat>();
        var index = playerInput.playerIndex;
        mover = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        combat = combaters.FirstOrDefault(c => c.GetPlayerIndex() == index);
    }
    
    public void OnMove(CallbackContext context)
    {
        if(mover != null)
            mover.SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnJump(CallbackContext context)
    {
       // Debug.Log(value);
       mover.gamepadJump();
    }

    public void OnPunch()
    {
       combat.GamepadPunch();
    }

    public void OnBlock(CallbackContext context)
    {
       combat.SetBlock(context.ReadValueAsButton());
    }

}