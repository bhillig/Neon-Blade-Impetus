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
        Context.playerRb.velocity = Vector3.zero;
        Context.playerRb.constraints |= (RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerRb.constraints = RigidbodyConstraints.FreezeRotation;
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

    }

    public override void CheckSwitchStates()
    {
        if(Context.inputContext.movementInput != Vector2.zero)
        {
            SwitchState(Factory.Run);
        }
    }
}
