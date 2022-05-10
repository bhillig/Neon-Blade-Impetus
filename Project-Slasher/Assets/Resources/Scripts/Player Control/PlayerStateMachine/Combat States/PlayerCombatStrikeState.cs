using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatStrikeState : PlayerCombatState
{
    public PlayerCombatStrikeState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) 
    {
        
    }

    public override void CheckSwitchState()
    {
        TrySwitchState(Factory.CombatIdle);
    }

    public override void EnterState()
    {
        Context.OnStrikeStart?.Invoke();
        Debug.Log("Strike");
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdateState()
    {
       
    }

    public override void UpdateState()
    {
        base.UpdateState();
        CheckSwitchState();
    }
}
