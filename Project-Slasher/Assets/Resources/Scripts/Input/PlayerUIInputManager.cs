using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIInputManager : MonoBehaviour, PlayerControls.IUIActions
{
    public static PlayerUIInputManager instance;
    public InputInfo movementInputInfo;
    private PlayerControls playerControls;

    public void Awake()
    {
        instance = this;
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.UI.SetCallbacks(this);
        movementInputInfo.lastNZeroMovementInput = Vector2.right;
    }

    public void OnDestroy()
    {
        playerControls.Disable();
        playerControls.Dispose();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementInputInfo.OptionsMenuDownEvent?.Invoke();
        }
    }
    public void SetEnabled(bool val)
    {
        if (val)
            playerControls.UI.Enable();
        else
            playerControls.UI.Disable();
    }
}
