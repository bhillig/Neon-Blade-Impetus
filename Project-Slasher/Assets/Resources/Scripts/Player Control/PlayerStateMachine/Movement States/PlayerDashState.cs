using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerDashState : PlayerMovementState
{
    public PlayerDashState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    protected float cachedSpeed;
    private bool subscribedToCollision;

    private int buffer = 0;

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log(Context.playerRb.velocity.magnitude);
        cachedSpeed = Context.playerRb.velocity.magnitude;
        Context.colliderSwitcher.SwitchToCollider(2);
        Context.OnStrikeEnd += StrikeDashEnd;
        subscribedToCollision = false;
        buffer = 0;
        Context.playerRb.useGravity = false;
    }

    protected override void PerformStrikeDash(Collider strikeHasTarget)
    {
        //Do nothing LOL
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.colliderSwitcher.SwitchToCollider(0);
        Context.OnStrikeEnd -= StrikeDashEnd;
        if(subscribedToCollision)
            Context.colliderEvents.OnCollisionEnterEvent -= OnDashCollision;
        Context.playerRb.useGravity = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {
        if (buffer == 2)
        {
            subscribedToCollision = true;
            Context.colliderEvents.OnCollisionEnterEvent += OnDashCollision;
        }
        buffer++;
    }

    protected void OnDashCollision(Collision coll)
    {
        StrikeDashEnd();
    }

    protected virtual void StrikeDashEnd()
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

}
