using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatChargeState : PlayerCombatState
{
    public PlayerCombatChargeState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) 
    {
        
    }

    private float timer = 0f;
    private bool fullyCharged;
    private bool overCharged;

    public override void CheckSwitchState()
    {
        if(overCharged)
        {
            TrySwitchState(Factory.CombatIdle);
        }
    }

    public override void EnterState()
    {
        timer = 0f;
        Context.primaryAttackCooldownTimer = Context.combatProfile.Cooldown;
        Context.inputContext.PrimaryUpEvent.AddListener(ChargeReleased);
    }

    public override bool IsStateSwitchable()
    {
        return Context.primaryAttackCooldownTimer <= 0;
    }

    public override void ExitState()
    {
        Context.primaryAttackCooldownTimer = Context.combatProfile.Cooldown;
        Context.inputContext.PrimaryUpEvent.RemoveListener(ChargeReleased);
    }
    
    protected void ChargeReleased()
    {
        if (fullyCharged)
            TrySwitchState(Factory.CombatStrike);
        else
            TrySwitchState(Factory.CombatIdle);
    }

    public override void FixedUpdateState()
    {
       
    }

    public override void UpdateState()
    {
        base.UpdateState();   
        timer += Time.deltaTime;
        fullyCharged = timer >= Context.combatProfile.ChargeTime;
        overCharged = timer >= Context.combatProfile.ChargeTime + Context.combatProfile.HoldTime;
        CheckSwitchState();
    }
}
