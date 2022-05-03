using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory 
{
    private PlayerStateMachine context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(context,this);
    }

    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(context, this);
    }

    public PlayerBaseState Run()
    {
        return new PlayerRunState(context, this);
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(context, this);
    }
    public PlayerBaseState Airborne()
    {
        return new PlayerAirborneState(context, this);
    }

    public PlayerMovementState Movement()
    {
        return new PlayerMovementState(context, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(context, this);
    }
}
