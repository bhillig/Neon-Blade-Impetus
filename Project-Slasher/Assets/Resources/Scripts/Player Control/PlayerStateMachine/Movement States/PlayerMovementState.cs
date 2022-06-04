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
        Context.playerEvents.OnCombatKilled += PlayerKilled;
    }

    public override void ExitState()
    {
        Context.playerEvents.OnStrikeStart -= PerformStrikeDash;
        Context.playerEvents.OnCombatKilled -= PlayerKilled;
    }

    public override void UpdateState()
    {
        Context.slideCooldownTimer -= Time.deltaTime;
        UpdateWindAudio();
    }

    protected virtual void UpdateWindAudio()
    {
        float speed = Context.playerRb.velocity.magnitude;
        float windEffect = Mathf.InverseLerp(0f, Context.movementProfile.TopMoveSpeed, speed);
        float currentWindEffect = PlayerAudioManager.GetGobalParameter("WindEffect");
        currentWindEffect = Mathf.MoveTowards(currentWindEffect, windEffect, 1.4f * Time.deltaTime);
        PlayerAudioManager.SetGlobalParameter("WindEffect", currentWindEffect);
    }

    protected virtual void PerformStrikeDash(AbstractEnemyEntity strikeHasTarget)
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
