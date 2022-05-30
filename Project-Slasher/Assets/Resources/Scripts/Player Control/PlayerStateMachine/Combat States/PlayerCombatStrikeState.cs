using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatStrikeState : PlayerCombatState
{
    public PlayerCombatStrikeState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) 
    {
        
    }

    private float timer = 0f;

    public override void EnterState()
    {
        Collider targetFound = SearchForTarget();
        Context.combatTarget = targetFound;
        // Invoke event to tell the movement state machine to switch to dash state
        Debug.Log("HITTTT");
        Context.playerEvents.OnStrikeStart?.Invoke(targetFound);
        // Calculate dash duration from distance and velocity
        if(targetFound == null)
        {
            timer = Context.combatProfile.DryDashDistance / Context.combatProfile.DryVelocity;
        }
        else
        {
            Context.primaryAttackCooldownTimer = 0f;
            // Calculate distance to target + pierce distance
            float dist = (targetFound.bounds.center - dashCollider.bounds.center).magnitude + Context.combatProfile.HitDashPierceDistance;
            timer = dist / Context.combatProfile.HitVelocity;
        }
        Context.groundPhysicsContext.GroundedBlockTimer = timer;
        // Animation
        Context.animationController.SetBool("Striking", true);
    }

    public override void ExitState()
    {
        Context.playerEvents.OnStrikeEnd?.Invoke();
        Context.animationController.SetBool("Striking", false);
    }

    public override void FixedUpdateState()
    {
        timer -= Time.fixedDeltaTime;
        CheckSwitchState();
    }
    public override void CheckSwitchState()
    {
        if (timer <= 0f)
            TrySwitchState(Factory.CombatIdle);
    }


    public override void UpdateState()
    {
        base.UpdateState();
    }
}
