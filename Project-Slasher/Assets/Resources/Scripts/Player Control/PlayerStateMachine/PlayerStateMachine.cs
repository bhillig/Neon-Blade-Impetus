using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerBaseState currentState;
    private PlayerStateFactory stateFactory;

    public PlayerBaseState CurrentState
    {
        get => currentState;
        set => currentState = value;    
    }

    private void Awake()
    {
        stateFactory = new PlayerStateFactory(this);
    }

    private void Update()
    {
        currentState.UpdateStates();
    }
}
