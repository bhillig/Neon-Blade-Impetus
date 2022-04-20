using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    PlayerControls playerControls;

    Vector2 movementVector;

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += input => movementVector = input.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
