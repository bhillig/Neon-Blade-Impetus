using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementState : PlayerBaseState
{
    public PlayerMovementState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) 
    {
        flatMove = context.flatMove;
    }

    protected FlatMovingStateComponent flatMove;

    public override void EnterState()
    {
        Context.playerEvents.OnStrikeStart += PerformStrikeDash;
        Context.playerEvents.OnCollideWithProjectile += PlayerKilled;
        Context.playerEvents.OnCollideWithVoid += PlayerKilled;
    }

    public override void ExitState()
    {
        Context.playerEvents.OnStrikeStart -= PerformStrikeDash;
        Context.playerEvents.OnCollideWithProjectile -= PlayerKilled;
        Context.playerEvents.OnCollideWithVoid -= PlayerKilled;
    }

    public override void UpdateState()
    {
        Context.slideCooldownTimer -= Time.deltaTime;
    }
    protected virtual void PerformStrikeDash(Collider strikeHasTarget)
    {
        if (strikeHasTarget != null)
        {
            TrySwitchState(Factory.TargetStrikeDash);
        }
        else
        {
            TrySwitchState(Factory.DryStrikeDash);
        }
    }

    protected virtual void PlayerKilled()
    {
        TrySwitchState(Factory.Dead);
    }
}
