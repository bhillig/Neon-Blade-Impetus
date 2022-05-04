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
        Context.inputContext.SpacebarDownEvent.RemoveListener(Jump);
    }

    private void Jump()
    {
        Context.playerRb.velocity += Vector3.up * Context.movementProfile.JumpVelocity;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void InitializeSubState()
    {
        SwitchSubState(this.Factory.Idle);
    }

    public override void CheckSwitchStates()
    {
        //Grounded check
    }
}
