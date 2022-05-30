using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerMovementState
{
    public PlayerDeadState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) { }

    public override void EnterState()
    {
        base.EnterState();
        Context.playerRb.constraints = RigidbodyConstraints.FreezeAll;
        Context.playerEvents.OnRestartLevel += MovementRestartLevel;
        CheckSwitchState();
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        Context.playerEvents.OnRestartLevel -= MovementRestartLevel;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    protected virtual void MovementRestartLevel()
    {
        TrySwitchState(Factory.Idle);
    }

    public override void FixedUpdateState()
    {

    }

    public override void CheckSwitchState()
    {

    }
    protected override void PlayerKilled()
    {
        // Do nothing
    }
    protected override void PerformStrikeDash(Collider strikeHasTarget)
    {
        // Do nothing
    }
}
