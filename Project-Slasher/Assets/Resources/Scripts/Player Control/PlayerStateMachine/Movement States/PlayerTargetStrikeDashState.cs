using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetStrikeDashState : PlayerDashState
{
    public PlayerTargetStrikeDashState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) { }

    private bool impactEnded;
    private Vector3 exitVelocity;
    private Vector3 impactEndVelocity;
    private float exitTimer = 0f;
    private float exitDuration = 0f;

    public override void EnterState()
    {
        base.EnterState();
        impactEnded = false;
        exitTimer = 0f;
        Context.colliderSwitcher.SwitchToCollider(4);
        Vector3 dashDirection = (Context.combatTarget.transform.position - Context.playerCenter.position).normalized;
        // Weight the saved velocity less if it's in the wrong direction
        cachedSpeed *= Mathf.Sqrt((Vector3.Dot(Context.playerRb.velocity.normalized, dashDirection) + 1f) / 2f);
        Context.playerPhysicsTransform.forward = dashDirection;
        Context.forwardVector = dashDirection;
        Context.playerRb.velocity = dashDirection * Context.combatProfile.HitVelocity;
        Context.playerEvents.ImpactEnd += CalculateExitVelocity;
    }

    public override void ExitState()
    {
        base.ExitState();
        if (!impactEnded)
            CalculateExitVelocity(0f);
        Context.playerRb.velocity = exitVelocity;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        if(impactEnded)
        {
            exitTimer += Time.fixedDeltaTime;
            float t = exitTimer / exitDuration;
            Context.playerRb.velocity = Vector3.Lerp(impactEndVelocity, exitVelocity, t);
        }
    }

    public override void CheckSwitchState()
    {
            
    }

    protected virtual void CalculateExitVelocity(float remainingTime)
    {
        impactEnded = true;
        impactEndVelocity = Context.playerRb.velocity;
        exitDuration = remainingTime;

        Vector3 dir = Context.playerRb.velocity.normalized;
        Vector3 boostOffset = Context.mainCam.transform.forward * Context.combatProfile.HitBoostVelocity;
        exitVelocity = dir * cachedSpeed + boostOffset;
        float dot = Vector3.Dot(dir, exitVelocity);
        if (dot < 4f)
            exitVelocity = dir * Context.combatProfile.HitBoostVelocity;
    }
}
