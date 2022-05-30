using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatDeadState : PlayerCombatState
{
    public PlayerCombatDeadState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) { }

    public override void EnterState()
    {
        base.EnterState();
        Context.inputContext.RestartDownEvent.AddListener(CombatRestartLevel);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.inputContext.RestartDownEvent.RemoveListener(CombatRestartLevel);
    }

    public override void FixedUpdateState() 
    { 
    
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void CheckSwitchState() 
    { 
    
    }

    protected override void PlayerCombatKilled()
    {
        // Do nothing
    }
    protected virtual void CombatRestartLevel()
    {
        Context.playerEvents.OnRestartLevel?.Invoke();
        TrySwitchState(Factory.CombatIdle);
    }
}
