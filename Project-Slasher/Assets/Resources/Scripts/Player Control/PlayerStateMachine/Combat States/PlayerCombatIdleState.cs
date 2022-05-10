using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatIdleState : PlayerCombatState
{
    public PlayerCombatIdleState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) 
    {
        
    }

    public override void CheckSwitchState()
    {
        
    }

    public override void EnterState()
    {
        //Context.inputContext.PrimaryDownEvent.AddListener(ChargeStarted);
    }

    protected void ChargeStarted()
    {
        TrySwitchState(Factory.CombatCharge);
    }

    public override void ExitState()
    {
        //Context.inputContext.PrimaryDownEvent.RemoveListener(ChargeStarted);
    }

    public override void FixedUpdateState()
    {
       
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("Idle");
        if (Context.inputContext.primaryDown)
            ChargeStarted();
    }
}
