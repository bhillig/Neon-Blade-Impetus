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
    }

    public void OnDestroy()
    {
        playerControls.Disable();
        playerControls.Dispose();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInputInfo.movementInput = context.ReadValue<Vector2>();
    }

    public void OnMouseMovement(InputAction.CallbackContext context)
    {
        movementInputInfo.mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnSpacebar(InputAction.CallbackContext context)
    {
        if (context.canceled)
            movementInputInfo.SpacebarUpEvent.Invoke();
        else if(context.performed)
            movementInputInfo.SpacebarDownEvent.Invoke();
    }
}
