using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInputManager : MonoBehaviour, PlayerControls.IPlayerMovementActions
{
    public InputInfo movementInputInfo;
    private PlayerControls playerControls;
    public void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.PlayerMovement.SetCallbacks(this);
        movementInputInfo.lastNZeroMovementInput = Vector2.right;
    }

    public void OnDestroy()
    {
        playerControls.Disable();
        playerControls.Dispose();
    }

    void Update()
    {
        if (movementInputInfo.shiftDown)
            movementInputInfo.ShiftHeldEvent.Invoke();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInputInfo.movementInput = context.ReadValue<Vector2>();
        if (movementInputInfo.movementInput != Vector2.zero)
            movementInputInfo.lastNZeroMovementInput = movementInputInfo.movementInput;
    }

    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        movementInputInfo.mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnSpacebar(InputAction.CallbackContext context)
    {
        if (context.canceled)
            movementInputInfo.SpacebarUpEvent.Invoke();
        else if(context.started)
            movementInputInfo.SpacebarDownEvent.Invoke();
    }

    public void OnShift(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            movementInputInfo.ShiftUpEvent.Invoke();
            movementInputInfo.shiftDown = false;
        }
        else if (context.started)
        {
            movementInputInfo.ShiftDownEvent.Invoke();
            movementInputInfo.ShiftHeldEvent.Invoke();
            movementInputInfo.shiftDown = true;
        }
    }
}
