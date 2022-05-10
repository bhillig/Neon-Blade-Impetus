using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : PlayerStateMachine
{

    public PlayerMovementStateMachine(PlayerController context) : base(context)
    {
        // Entry state
        currentState = stateFactory.Idle;
        currentState.EnterState();
    }
}
