using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    public override void EnterState()
    {
        Context.animationController.SetBool("Airborne", true);
        InitializeSubState();
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Airborne", false);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void InitializeSubState()
    {
        SwitchSubState(Factory.Jump);
    }

    public override void CheckSwitchStates()
    {
        //Grounded check
        if (Context.physicsbodyContext.IsGrounded())
        {
            SwitchState(Factory.Grounded);
        }
    }
}
