using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) {}

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        Vector2 input = Context.inputContext.movementInput.normalized;
        var rb = Context.GetComponent<Rigidbody>();
        rb.velocity = 
            5 * Context.cameraControlContext.RotateAroundCameraY(input.ToXZPlane())
            + rb.velocity.y * Vector3.up;
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {

    }
}
