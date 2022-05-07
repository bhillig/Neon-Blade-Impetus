using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) { }

    public override void EnterState()
    {
        base.EnterState();
        Context.playerRb.velocity = Vector3.zero;
        Context.playerRb.constraints |= (RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ);
        CheckSwitchState();
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerRb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        Context.groundPhysicsContext.DisplayGroundVectors();
        flatMove.LerpRotation(Context.movementProfile.TurnSpeed);
    }


    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if(Context.inputContext.movementInput != Vector2.zero)
        {
            TrySwitchState(Factory.Run);
        }
    }
}
