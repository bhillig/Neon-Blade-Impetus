using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatChargeState : PlayerCombatState
{
    public PlayerCombatChargeState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) { }

    private float timer = 0f;
    private bool fullyCharged;
    private bool overCharged;

    public override void EnterState()
    {
        base.EnterState();
        timer = 0f;
        fullyCharged = false;
        overCharged = false;
        Context.primaryAttackCooldownTimer = Context.combatProfile.Cooldown;
        Context.inputContext.PrimaryUpEvent.AddListener(ChargeReleased);
        Context.animationController.SetBool("Charging", true);
        Context.playerEvents.OnStrikeChargeStart?.Invoke();
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.inputContext.PrimaryUpEvent.RemoveListener(ChargeReleased);
        Context.animationController.SetBool("Charging", false);
    }

    public override bool IsStateSwitchable()
    {
        return Context.primaryAttackCooldownTimer <= 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        timer += Time.deltaTime;
        if (!fullyCharged && timer >= Context.combatProfile.ChargeTime)
        {
            fullyCharged = true;
            Context.playerEvents.OnStrikeChargeReady?.Invoke();
        }
        if (!overCharged && timer >= Context.combatProfile.ChargeTime + Context.combatProfile.HoldTime)
        {
            overCharged = true;
        }
        CheckSwitchState();
    }

    public override void FixedUpdateState() 
    { 
    
    }

    public override void CheckSwitchState()
    {
        if (overCharged)
        {
            Context.primaryAttackCooldownTimer = Context.combatProfile.Cooldown;
            Context.playerEvents.OnStrikeChargeEnd?.Invoke(false);
            Context.playerEvents.OnStrikeOvercharged?.Invoke();
            TrySwitchState(Factory.CombatIdle);
        }
    }

    protected override void PlayerCombatKilled()
    {
        Context.primaryAttackCooldownTimer = 0f;
        Context.playerEvents.OnStrikeChargeEnd?.Invoke(false);
        base.PlayerCombatKilled();
    }   

    protected void ChargeReleased()
    {
        if (fullyCharged)
        {
            Context.primaryAttackCooldownTimer = Context.combatProfile.Cooldown;
            Context.playerEvents.OnStrikeChargeEnd?.Invoke(true);
            TrySwitchState(Factory.CombatStrike);
        }
        else
        {
            Context.primaryAttackCooldownTimer = 0f;
            Context.playerEvents.OnStrikeChargeEnd?.Invoke(false);
            TrySwitchState(Factory.CombatIdle);
        }
    }
}
