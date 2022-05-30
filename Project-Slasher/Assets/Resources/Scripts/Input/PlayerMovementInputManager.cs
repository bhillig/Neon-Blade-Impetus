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

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.canceled)
            movementInputInfo.JumpUpEvent.Invoke();
        else if(context.started)
            movementInputInfo.JumpDownEvent.Invoke();
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            movementInputInfo.slideDown = false;
            movementInputInfo.SlideUpEvent.Invoke();
        }
        else if (context.started)
        {
            movementInputInfo.slideDown = true;
            movementInputInfo.SlideDownEvent.Invoke();
        }
    }

    public void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            movementInputInfo.primaryDown = false;
            movementInputInfo.PrimaryUpEvent.Invoke();
        }
        else if (context.started)
        {
            movementInputInfo.primaryDown = true;
            movementInputInfo.PrimaryDownEvent.Invoke();
        }
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementInputInfo.RestartDownEvent.Invoke();
        }
    }
}
