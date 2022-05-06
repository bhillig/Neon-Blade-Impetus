using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) {}

    public override void EnterState()
    {
        InitializeSubState();
        Context.inputContext.SpacebarDownEvent.AddListener(Jump);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.inputContext.SpacebarDownEvent.RemoveListener(Jump);
    }

    private void Jump()
    {
        Context.playerRb.velocity += Vector3.up * Context.movementProfile.JumpVelocity;
        Context.groundPhysicsContext.SnapToGroundBlock = 0.5f;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {

    }

    public override void InitializeSubState()
    {
        if(Context.playerRb.velocity.magnitude == 0f)
            SwitchSubState(this.Factory.Idle);
        else
            SwitchSubState(this.Factory.Stopping);
    }

    public override void CheckSwitchStates()
    {
        //Grounded check
        if (!Context.groundPhysicsContext.IsGrounded())
        {
            SwitchState(Factory.Airborne);
        }
    }
}
