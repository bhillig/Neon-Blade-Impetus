using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatStateMachine : PlayerStateMachine
{

    public PlayerCombatStateMachine(PlayerController context) : base(context)
    {
        // Entry state
        currentState = stateFactory.CombatIdle;
        currentState.EnterState();
    }
}
