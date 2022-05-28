using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAirborneState : PlayerMovementState
{
    Wallrunning wallrunning;

    public PlayerAirborneState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
         
    }

    public override void EnterState()
    {
        base.EnterState();
        Context.animationController.SetBool("Airborne", true);
        Context.inputContext.JumpDownEvent.AddListener(OnSpacebarDown);
        wallrunning = new Wallrunning(Context);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Airborne", false);
        Context.inputContext.JumpDownEvent.RemoveListener(OnSpacebarDown);
    }

    public void OnSpacebarDown()
    {
        if(Context.wallFinder.SearchForWall(Context.movementProfile.MinGroundedDotProd) != null)
        {
            TrySwitchState(Factory.Wallglide);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void CheckSwitchState()
    {
        // wallrun check
        wallrunning.DetectWalls();
        if (wallrunning.ShouldWallRun())
        {
            TrySwitchState(Factory.Wallglide);
        }

        //Grounded check
        if (Context.groundPhysicsContext.IsGrounded())
        {
            TrySwitchState(Factory.GroundedSwitch);
        }
    }
}
