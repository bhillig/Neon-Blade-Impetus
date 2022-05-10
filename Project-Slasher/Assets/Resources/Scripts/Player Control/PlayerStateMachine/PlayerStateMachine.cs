using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateMachine : StateMachine.IStateMachineContext
{
    // Context
    protected PlayerController context;
    public PlayerController Context => context;

    protected PlayerBaseState currentState;
    protected PlayerStateFactory stateFactory;

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
    }

    public virtual void UpdateStateMachine()
    {
        currentState.UpdateState();
    }

    public virtual void FixedUpdateStateMachine()
    {
        currentState.FixedUpdateState();
    }
}
