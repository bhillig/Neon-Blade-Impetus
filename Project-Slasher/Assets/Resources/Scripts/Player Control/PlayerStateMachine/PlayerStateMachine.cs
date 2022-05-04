using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine.IStateMachineContext
{
    // Context
    private PlayerController context;
    public PlayerController Context => context;

    private PlayerBaseState currentState;
    private PlayerStateFactory stateFactory;

    public IState CurrentState
    {
        get => currentState;
        set => currentState = (PlayerBaseState)value;    
    }

    public PlayerStateMachine(PlayerController context)
    {
        this.context = context;
        stateFactory = new PlayerStateFactory(this);
        //Entry state
        currentState = stateFactory.Movement;
        currentState.EnterState();
    }

    public void UpdateStateMachine()
    {
        currentState.UpdateStates();
    }

    public void FixedUpdateStateMachine()
    {
        currentState.FixedUpdateStates();
    }
}
