using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used as a "generic" grounded entry state that instantly switches to a proper state
/// </summary>
public class PlayerGroundedSwitchState : PlayerGroundedState
{
    public PlayerGroundedSwitchState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) {}

    public override void EnterState()
    {
        CheckSwitchState();
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }

    public override void CheckSwitchState()
    {
        if(Context.inputContext.shiftDown)
        {
            if (TrySwitchState(Factory.Slide))
                return;
        }
        if (Context.inputContext.movementInput == Vector2.zero)
        {
            if (TrySwitchState(Factory.Stopping))
                return;
        }
        else
        {
            TrySwitchState(Factory.Run);
        }
    }
}
