using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCombatState : PlayerBaseState
{
    public PlayerCombatState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) 
    {
        dashCollider = (CapsuleCollider)Context.colliderSwitcher.GetCollider(2);
    }

    protected CapsuleCollider dashCollider;

    public override void EnterState()
    {
        Context.playerEvents.OnCollideWithProjectile += PlayerCombatKilled;
        Context.playerEvents.OnCollideWithVoid += PlayerCombatKilled;
    }

    public override void ExitState()
    {
        Context.playerEvents.OnCollideWithProjectile -= PlayerCombatKilled;
        Context.playerEvents.OnCollideWithVoid -= PlayerCombatKilled;
    }

    public override void UpdateState()
    {       
        if(Context.primaryAttackCooldownTimer > 0f)
        {
            Context.primaryAttackCooldownTimer -= Time.deltaTime;
            if(Context.primaryAttackCooldownTimer < 0f)
            {
                Context.audioManager.cooldownUp.Play();
                Context.playerEvents.OnStrikeCooldownFinished?.Invoke();
            }
        }
        Context.combatTarget = SearchForTarget();
        CursorScript.instance.SetCursorState(Context.combatTarget == null ? CursorStates.Default : CursorStates.Enemy);
    }

    protected virtual AbstractEnemyEntity SearchForTarget()
    {
        Vector3 dir = Camera.main.transform.forward;
        // First do a raw camera raycast to find the player's intended target
        if(Physics.SphereCast(
                Camera.main.transform.position,
                Context.combatProfile.CastRadius,
                dir,
                out RaycastHit rawTarget,
                100,
                Context.combatProfile.TargetMask
            ))
        {
            var foundEnemyTarget = rawTarget.collider.GetComponentInParent<AbstractEnemyEntity>();
            if (foundEnemyTarget == null)
                return null;
            // Then do an actual raycast to see if there is a clear shot
            Vector3 validCastDir = (foundEnemyTarget.Center.position - dashCollider.bounds.center).normalized;
            if (Physics.SphereCast(
                dashCollider.bounds.center,
                dashCollider.radius,
                validCastDir,
                out RaycastHit validTarget,
                Context.combatProfile.CastDistance,
                Context.combatProfile.HitMask
            ))
            {
                if (validTarget.collider.gameObject.IsInLayerMask(Context.combatProfile.TargetMask))
                {
                    return foundEnemyTarget;
                }
            }
        }
        return null;
    }
    protected virtual void PlayerCombatKilled()
    {
        Context.playerEvents.OnCombatKilled?.Invoke();
        TrySwitchState(Factory.CombatDead);
    }
}
