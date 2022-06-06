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
        Context.animationController.SetBool("Charging", true);
        Context.playerEvents.OnStrikeChargeStart?.Invoke();
        Context.audioManager.chargeupWhineEmitter.Play();
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Charging", false);
        Context.audioManager.chargeupWhineEmitter.Stop();
        Context.audioManager.chargeupReadyEmitter.Stop();
    }

    public override bool IsStateSwitchable()
    {
        return Context.primaryAttackCooldownTimer <= 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        timer += Time.deltaTime;
        PlayerAudioManager.SetGlobalParameter("ChargeupTimeRatio", timer / Context.combatProfile.ChargeTime);
        if (!fullyCharged && timer >= Context.combatProfile.ChargeTime)
        {
            fullyCharged = true;
            Context.audioManager.chargeupWhineEmitter.Stop();
            Context.audioManager.chargeupReadyEmitter.Play();
            Context.playerEvents.OnStrikeChargeReady?.Invoke();
        }
        if (!overCharged && timer >= Context.combatProfile.ChargeTime + Context.combatProfile.HoldTime)
        {
            overCharged = true;
        }
        if (!Context.inputContext.primaryDown)
            ChargeReleased();
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
            Context.audioManager.chargeupOvercharge.Play();
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
            Context.audioManager.chargeupWhineEmitter.Stop();
            TrySwitchState(Factory.CombatIdle);
        }
    }
}
