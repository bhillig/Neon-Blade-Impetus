using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatIdleState : PlayerCombatState
{
    public PlayerCombatIdleState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) 
    {
        
    }

    public override void CheckSwitchState()
    {
        
    }

    public override void EnterState()
    {
        //Context.inputContext.PrimaryDownEvent.AddListener(ChargeStarted);
        if (Context.primaryAttackCooldownTimer > 0f)
            Context.playerEvents.OnStrikeCooldownStarted?.Invoke();
        Context.inputContext.PrimaryDownEvent.AddListener(ChargeStarted);
    }

    protected void ChargeStarted()
    {
        TrySwitchState(Factory.CombatCharge);
    }

    public override void ExitState()
    {
        Context.inputContext.PrimaryDownEvent.RemoveListener(ChargeStarted);
    }

    public override void FixedUpdateState()
    {
       
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
