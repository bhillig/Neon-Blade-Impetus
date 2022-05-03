using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory 
{
    private PlayerStateMachine context;
    Dictionary<System.Type,PlayerBaseState> statePool = new Dictionary<System.Type, PlayerBaseState>();

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
        //Initialize pool
        statePool.Add(typeof(PlayerIdleState), new PlayerIdleState(context, this));
        statePool.Add(typeof(PlayerRunState), new PlayerRunState(context, this));
        statePool.Add(typeof(PlayerGroundedState), new PlayerGroundedState(context, this));
        statePool.Add(typeof(PlayerAirborneState), new PlayerAirborneState(context, this));
        statePool.Add(typeof(PlayerMovementState), new PlayerMovementState(context, this));
        statePool.Add(typeof(PlayerJumpState), new PlayerJumpState(context, this));
    }

    public PlayerBaseState Idle()
    {
        return statePool[typeof(PlayerIdleState)];
    }

    public PlayerBaseState Run()
    {
        return statePool[typeof(PlayerRunState)];
    }

    public PlayerBaseState Grounded()
    {
        return statePool[typeof(PlayerGroundedState)];
    }
    public PlayerBaseState Airborne()
    {
        return statePool[typeof(PlayerAirborneState)];
    }

    public PlayerBaseState Movement()
    {
        return statePool[typeof(PlayerMovementState)];
    }

    public PlayerBaseState Jump()
    {
        return statePool[typeof(PlayerJumpState)];
    }
}
