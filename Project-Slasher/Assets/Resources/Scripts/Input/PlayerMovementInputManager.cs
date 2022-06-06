using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInputManager : MonoBehaviour, PlayerControls.IPlayerMovementActions
{
    public static PlayerMovementInputManager instance;
    public InputInfo movementInputInfo;
    private PlayerControls playerControls;

    private float jumpDownTimer = 0f;
    private float primaryDownTimer = 0f;

    public void Awake()
    {
        instance = this;
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

    public void SetEnabled(bool val)
    {
        if (val)
            playerControls.PlayerMovement.Enable();
        else
            playerControls.PlayerMovement.Disable();
    }

    void Update()
    {
        jumpDownTimer -= Time.deltaTime;
        primaryDownTimer -= Time.deltaTime;
        if (jumpDownTimer > 0f)
            movementInputInfo.JumpDownEvent?.Invoke();
        if (primaryDownTimer > 0f)
            movementInputInfo.PrimaryDownEvent?.Invoke();
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
        {
            movementInputInfo.JumpUpEvent.Invoke();
        }
        else if(context.started)
        {
            movementInputInfo.JumpDownEvent.Invoke();
            jumpDownTimer = 0.15f;
        }
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
            primaryDownTimer = 0.15f;
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

    public void OnMaskRotate(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementInputInfo.MaskRotateDownEvent.Invoke();
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementInputInfo.OptionsMenuDownEvent?.Invoke();
        }
    }
}
