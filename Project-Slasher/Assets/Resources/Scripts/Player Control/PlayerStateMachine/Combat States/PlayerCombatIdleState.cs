using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatIdleState : PlayerCombatState
{
    public PlayerCombatIdleState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) { }

    public override void EnterState()
    {
        base.EnterState();
        //Context.inputContext.PrimaryDownEvent.AddListener(ChargeStarted);
        if (Context.primaryAttackCooldownTimer > 0f)
            Context.playerEvents.OnStrikeCooldownStarted?.Invoke();
        Context.inputContext.PrimaryDownEvent.AddListener(ChargeStarted);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.inputContext.PrimaryDownEvent.RemoveListener(ChargeStarted);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {

    }

    public override void CheckSwitchState()
    {

    }

    protected void ChargeStarted()
    {
        TrySwitchState(Factory.CombatCharge);
    }
}
