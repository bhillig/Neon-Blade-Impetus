using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory)
    {

    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if(Context.inputContext.movementInput != Vector2.zero)
        {
            SwitchState(Factory.Run);
        }
    }
}
