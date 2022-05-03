using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, StateMachine.IStateMachineContext
{
    private PlayerBaseState currentState;
    private PlayerStateFactory stateFactory;

    public IState CurrentState
    {
        get => currentState;
        set => currentState = (PlayerBaseState)value;    
    }

    private void Awake()
    {
        stateFactory = new PlayerStateFactory(this);
        currentState = stateFactory.Movement();
        currentState.EnterState();
    }

    private void Update()
    {
        currentState.UpdateStates();
    }
}
