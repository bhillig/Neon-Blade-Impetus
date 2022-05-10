using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCombatState : PlayerBaseState
{
    public PlayerCombatState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) 
    {
        
    }

    public override void UpdateState()
    {
        Context.primaryAttackCooldownTimer -= Time.deltaTime;
    }
}
