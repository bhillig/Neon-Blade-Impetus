using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : PlayerBaseState
{
    public PlayerMovementState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) {}

    public override void EnterState()
    {
        InitializeSubState();
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }

    public override void InitializeSubState()
    {
        SwitchSubState(this.Factory.Grounded);
    }

    public override void CheckSwitchStates()
    {

    }
}
