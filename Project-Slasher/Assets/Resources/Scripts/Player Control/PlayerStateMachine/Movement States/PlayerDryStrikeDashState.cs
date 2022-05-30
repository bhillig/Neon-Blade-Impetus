using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDryStrikeDashState : PlayerDashState
{
    public PlayerDryStrikeDashState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    private float entryVel;

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Dry strike dash");
        Vector3 forward = Camera.main.transform.forward;
        entryVel = Context.playerRb.velocity.magnitude * 
            Mathf.Max(0,Vector3.Dot(forward, Context.playerRb.velocity.normalized));
        Context.playerRb.velocity = forward * Context.combatProfile.DryVelocity;
        Context.playerPhysicsTransform.forward = forward;
        Context.forwardVector = forward;
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerRb.velocity = Context.playerRb.velocity.normalized * 
            Mathf.Max(entryVel,Context.combatProfile.DryExitVelocity);
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
