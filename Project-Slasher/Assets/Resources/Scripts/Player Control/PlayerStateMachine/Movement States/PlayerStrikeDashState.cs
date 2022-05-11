using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikeDashState : PlayerMovementState
{
    public PlayerStrikeDashState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    private float cachedSpeed;

    public override void EnterState()
    {
        Context.animationController.SetBool("Airborne", true);
        Context.OnStrikeEnd += StrikeDashEnd;
        cachedSpeed = Context.playerRb.velocity.magnitude;
        Context.playerRb.useGravity = false;
        Context.playerRb.velocity = Camera.main.transform.forward * Context.combatProfile.Velocity;
    }

    public override void ExitState()
    {
        Context.animationController.SetBool("Airborne", false);
        Context.OnStrikeEnd -= StrikeDashEnd;
        Context.playerRb.useGravity = true;
        Context.playerRb.velocity = Context.playerRb.velocity.normalized * cachedSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void StrikeDashEnd()
    {
        if (Context.groundPhysicsContext.IsGrounded())
        {
            TrySwitchState(Factory.GroundedSwitch);
        }
        else
        {
            TrySwitchState(Factory.Jump);
        }
    }

    public override void CheckSwitchState()
    {
            
    }

    public override void FixedUpdateState()
    {
        
    }
}
