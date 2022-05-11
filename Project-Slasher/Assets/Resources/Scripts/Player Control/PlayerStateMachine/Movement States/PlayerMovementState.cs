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
        Context.OnStrikeStart += PerformStrikeDash;
    }

    public override void ExitState()
    {
        Context.OnStrikeStart -= PerformStrikeDash;
    }

    private void PerformStrikeDash()
    {
        TrySwitchState(Factory.StrikeDash);
    }

    public override void UpdateState()
    {
        Context.slideCooldownTimer -= Time.deltaTime;
    }
}
