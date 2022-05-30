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
    }

    public override void ExitState()
    {
        Context.playerEvents.OnStrikeStart -= PerformStrikeDash;
    }

    protected virtual void PerformStrikeDash(Collider strikeHasTarget)
    {
        if(strikeHasTarget != null)
        {
            TrySwitchState(Factory.HitStrikeDash);
        }
        else
        {
            Debug.Log("E");
            TrySwitchState(Factory.DryStrikeDash);
        }
    }

    public override void UpdateState()
    {
        Context.slideCooldownTimer -= Time.deltaTime;
    }
}
