using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallglideState : PlayerMovementState
{
    public PlayerWallglideState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    private WallSearchResult results;
    private Wallrunning wallrunning;
    public override void EnterState()
    {
        Context.playerRb.useGravity = false;
        Context.animationController.SetBool("Airborne", true);
        Context.inputContext.SpacebarDownEvent.AddListener(OnSpacebarDown);
        wallrunning = new Wallrunning(Context);
        wallrunning.StartWallRun();
    }

    public override void ExitState()
    {
        Context.playerRb.useGravity = true;
        Context.animationController.SetBool("Airborne", false);
        Context.inputContext.SpacebarDownEvent.RemoveListener(OnSpacebarDown);
    }

    public void OnSpacebarDown()
    {
        wallrunning.JumpFromWall();
        TrySwitchState(Factory.Jump);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        wallrunning.CheckForWall();
        wallrunning.CheckDuration();
    }

    public override void FixedUpdateState()
    {
        if (wallrunning.IsWallRunning())
        {
            wallrunning.WallRunningMovement();
        }
        
    }

    public override void CheckSwitchState()
    {
        if (!wallrunning.IsWallRunning())
        {
            TrySwitchState(Factory.Jump);
            return;
        }

        //Grounded check
        if (Context.groundPhysicsContext.IsGrounded())
        {
            TrySwitchState(Factory.Idle);
        }
    }
}
