using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    private PlayerStateMachine context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
        // Initialize states
        Wallglide = new PlayerWallglideState(context, this);

        Idle = new PlayerIdleState(context, this);
        Jump = new PlayerJumpState(context, this);
        Slide = new PlayerSlideState(context, this);
        Stopping = new PlayerStoppingState(context, this);
        Landing = new PlayerLandingState(context, this);
        Run = new PlayerRunState(context, this);
        StrikeDash = new PlayerStrikeDashState(context, this);

        // Switch states
        GroundedSwitch = new PlayerGroundedSwitchState(context, this);

        // Combat states
        CombatIdle = new PlayerCombatIdleState(context, this);
        CombatCharge = new PlayerCombatChargeState(context, this);
        CombatStrike = new PlayerCombatStrikeState(context, this);
    }

    public PlayerBaseState Wallglide { get; }

    public PlayerBaseState Idle { get; }
    public PlayerBaseState Stopping { get; }
    public PlayerBaseState Landing { get; }
    public PlayerBaseState Run { get; }
    public PlayerBaseState Jump { get; }
    public PlayerBaseState Slide { get; }
    public PlayerBaseState StrikeDash { get; }

    // Switch states
    public PlayerBaseState GroundedSwitch { get; }

    // Combat states
    public PlayerBaseState CombatIdle { get; }
    public PlayerBaseState CombatCharge { get; }
    public PlayerBaseState CombatStrike { get; }
}

