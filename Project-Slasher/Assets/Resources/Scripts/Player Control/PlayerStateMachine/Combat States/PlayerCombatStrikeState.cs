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
        Context.OnStrikeStart?.Invoke(targetFound);
        // Calculate dash duration from distance and velocity
        if(targetFound == null)
        {
            timer = Context.combatProfile.DryDashDistance / Context.combatProfile.DryVelocity;
        }
        else
        {
            // Calculate distance to target + pierce distance
            float dist = (targetFound.bounds.center - dashCollider.bounds.center).magnitude + Context.combatProfile.HitDashPierceDistance;
            timer = dist / Context.combatProfile.HitVelocity;
        }
        Context.groundPhysicsContext.SnapToGroundBlock = timer;
    }

    public override void ExitState()
    {
        Context.OnStrikeEnd?.Invoke();
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
