using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatStrikeState : PlayerCombatState
{
    public PlayerCombatStrikeState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) 
    {
        
    }

    private float timer = 0f;

    public override void CheckSwitchState()
    {
        if (timer <= 0f)
            TrySwitchState(Factory.CombatIdle);
    }

    public override void EnterState()
    {
        Context.OnStrikeStart?.Invoke();
        timer = Context.combatProfile.Duration;
        Debug.Log("Strike");
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

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
