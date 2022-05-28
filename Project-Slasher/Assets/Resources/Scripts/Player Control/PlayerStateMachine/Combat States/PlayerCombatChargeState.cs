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
            Context.playerEvents.OnStrikeChargeEnd?.Invoke(false);
            TrySwitchState(Factory.CombatIdle);
        }
    }

    public override void EnterState()
    {
        timer = 0f;
        fullyCharged = false;
        overCharged = false;
        Context.primaryAttackCooldownTimer = Context.combatProfile.Cooldown;
        Context.inputContext.PrimaryUpEvent.AddListener(ChargeReleased);
        Context.animationController.SetBool("Charging", true);
    }

    public override bool IsStateSwitchable()
    {
        return Context.primaryAttackCooldownTimer <= 0;
    }

    public override void ExitState()
    {
        Context.primaryAttackCooldownTimer = Context.combatProfile.Cooldown;
        Context.inputContext.PrimaryUpEvent.RemoveListener(ChargeReleased);
        Context.animationController.SetBool("Charging", false);
    }
    
    protected void ChargeReleased()
    {
        if (fullyCharged)
        {
            Context.playerEvents.OnStrikeChargeEnd?.Invoke(true);
            TrySwitchState(Factory.CombatStrike);
        }
        else
        {
            Context.playerEvents.OnStrikeChargeEnd?.Invoke(false);
            TrySwitchState(Factory.CombatIdle);
            Context.primaryAttackCooldownTimer = 0f;
        }
    }

    public override void FixedUpdateState()
    {
       
    }

    public override void UpdateState()
    {
        base.UpdateState();   
        timer += Time.deltaTime;
        if(!fullyCharged && timer >= Context.combatProfile.ChargeTime)
        {
            fullyCharged = true;
            Context.playerEvents.OnStrikeChargeReady?.Invoke();
        }
        if(!overCharged &&  timer >= Context.combatProfile.ChargeTime + Context.combatProfile.HoldTime)
        {
            overCharged = true;
        }        
        CheckSwitchState();
    }
}
