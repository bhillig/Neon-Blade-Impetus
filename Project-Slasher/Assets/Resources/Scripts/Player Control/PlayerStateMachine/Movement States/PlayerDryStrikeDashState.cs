using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDryStrikeDashState : PlayerDashState
{
    public PlayerDryStrikeDashState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Dry strike dash");
        Vector3 forward = Camera.main.transform.forward;
        Context.playerRb.velocity = forward * Context.combatProfile.DryVelocity;
        Context.playerPhysicsTransform.forward = forward;
        Context.forwardVector = forward;
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerRb.velocity = Context.playerRb.velocity.normalized * Context.combatProfile.DryMinVelocity;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void CheckSwitchState()
    {
          
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
}
