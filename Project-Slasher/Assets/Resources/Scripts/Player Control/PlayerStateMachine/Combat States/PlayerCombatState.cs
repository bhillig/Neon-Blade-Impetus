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
        SetCursorCombatState();
    }

    protected virtual void SetCursorCombatState()
    {
        CursorScript.instance.SetCursorState(Context.combatTarget == null ? CursorStates.Default : CursorStates.Enemy);
    }

    protected virtual AbstractEnemyEntity SearchForTarget()
    {
        // First do a raw camera raycast and overlap to find the player's intended target
        Vector3 dir = Camera.main.transform.forward;
        Collider targetSearchResult = null;
        bool castFind = Physics.SphereCast(
                Camera.main.transform.position,
                Context.combatProfile.CastRadius,
                dir,
                out RaycastHit rawTarget,
                1000,
                Context.combatProfile.TargetMask
            );
        // Overlap in case the camera is inside the enemy
        if(!castFind)
        {
            var overlap = Physics.OverlapSphere(
                Camera.main.transform.position,
                Context.combatProfile.CastRadius,
                Context.combatProfile.TargetMask);
            if(overlap.Length > 0)
            {
                targetSearchResult = overlap[0];
            }
        }
        else
        {
            targetSearchResult = rawTarget.collider;
        }

        // Then do a terrain hitting raycast to see if there is a clear shot
        if (targetSearchResult != null)
        {
            var foundEnemyTarget = targetSearchResult.GetComponentInParent<AbstractEnemyEntity>();
            if (foundEnemyTarget == null)
                return null;
            Vector3 validCastDir = (foundEnemyTarget.Center.position - dashCollider.bounds.center).normalized;
            if (Physics.SphereCast(
                dashCollider.bounds.center,
                dashCollider.radius,
                validCastDir,
                out RaycastHit validTarget,
                Context.combatProfile.CastDistance - dashCollider.radius,
                Context.combatProfile.HitMask
            ))
            {
                if (validTarget.collider.gameObject.IsInLayerMask(Context.combatProfile.TargetMask))
                {
                    return validTarget.collider.GetComponentInParent<AbstractEnemyEntity>();
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
