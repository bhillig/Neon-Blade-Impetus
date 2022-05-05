using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    private PlayerStateMachine context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
        //Initialize states
        Movement = new PlayerMovementState(context, this);
        Grounded = new PlayerGroundedState(context, this);
        Airborne = new PlayerAirborneState(context, this);
        Wallglide = new PlayerWallglideState(context, this);

        Idle = new PlayerIdleState(context, this);
        Jump = new PlayerJumpState(context, this);
        Stopping = new PlayerStoppingState(context, this);
        Landing = new PlayerLandingState(context, this);
        Run = new PlayerRunState(context, this);

    }

    public PlayerBaseState Movement { get; }

    public PlayerBaseState Grounded { get; }
    public PlayerBaseState Airborne { get; }
    public PlayerBaseState Wallglide { get; }


    public PlayerBaseState Idle { get; }
    public PlayerBaseState Stopping { get; }
    public PlayerBaseState Landing { get; }
    public PlayerBaseState Run { get; }
    public PlayerBaseState Jump { get; }
}

