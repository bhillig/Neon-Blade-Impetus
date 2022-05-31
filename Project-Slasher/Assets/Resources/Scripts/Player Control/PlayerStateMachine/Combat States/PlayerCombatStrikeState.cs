using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatStrikeState : PlayerCombatState
{
    public PlayerCombatStrikeState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) { }

    private float timer = 0f;
    private float impactTimer = 0f;
    private AbstractEnemyEntity target;

    public override void EnterState()
    {
        base.EnterState();
        Collider targetFound = SearchForTarget();
        Context.combatTarget = targetFound;
        // Invoke event to tell the movement state machine to switch to dash state
        Context.playerEvents.OnStrikeStart?.Invoke(targetFound);
        // Save target
        target = targetFound == null ? null : targetFound.GetComponentInParent<AbstractEnemyEntity>();
        // Calculate dash duration from distance and velocity
        if (target == null)
        {
            timer = Context.combatProfile.DryDashDistance / Context.combatProfile.DryVelocity;
            // -1 means it will not tick down
            impactTimer = -1f;
        }
        else
        {
            Context.TPComponentController.SetDistance(4f);
            Context.primaryAttackCooldownTimer = 0f;
            // Calculate distance to target + pierce distance
            float dist = (targetFound.bounds.center - dashCollider.bounds.center).magnitude;
            float pierceDist = dist + Context.combatProfile.HitDashPierceDistance;
            timer = pierceDist / Context.combatProfile.HitVelocity;
            impactTimer = Mathf.Max(0f, dist / Context.combatProfile.HitVelocity + Context.combatProfile.TimeSlowStartOffset);
        }
        Context.groundPhysicsContext.GroundedBlockTimer = timer;
        // Animation
        Context.animationController.SetBool("Striking", true);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerEvents.OnStrikeEnd?.Invoke();
        Context.TPComponentController.ResetDistance();
        Context.animationController.SetBool("Striking", false);
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {
        timer -= Time.fixedDeltaTime;
        if (impactTimer >= 0f)
        {
            impactTimer -= Time.fixedDeltaTime;
            if(impactTimer < 0f)
                ImpactEffect();
        }
        CheckSwitchState();
    }
    public override void CheckSwitchState()
    {
        if (timer <= 0f)
        {
            TrySwitchState(Factory.CombatIdle);
        }
    }

    protected virtual void ImpactEffect()
    {
        KillEnemy();
        TimeScaleManager.instance.SetTimeScale(
            Context.combatProfile.TimeSlowScale, 
            Context.combatProfile.TimeSlowDuration, 
            0.05f,
            ImpactEnd
            );
    }

    protected virtual void ImpactEnd()
    {
        Context.playerEvents.ImpactEnd?.Invoke(timer - impactTimer);
    }

    protected virtual void KillEnemy()
    {
        target.KillEnemy();
        Context.colliderSwitcher.GetConcreteCollider().enabled = true;
    }

    protected override void PlayerCombatKilled()
    {
        // Do nothing, invincible while in this state
    }
}
