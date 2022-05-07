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

    public FlatMovingStateComponent flatMove;

    public IState CurrentState
    {
        get => currentState;
        set => currentState = (PlayerBaseState)value;    
    }

    public PlayerStateMachine(PlayerController context)
    {
        // Create state dependencies
        this.context = context;
        flatMove = new FlatMovingStateComponent(context);
        // Set up factory and default state
        stateFactory = new PlayerStateFactory(this);
        // Entry state
        currentState = stateFactory.Idle;
        currentState.EnterState();

    }

    public void UpdateStateMachine()
    {
        currentState.UpdateState();
    }

    public void FixedUpdateStateMachine()
    {
        currentState.FixedUpdateState();
    }
}
