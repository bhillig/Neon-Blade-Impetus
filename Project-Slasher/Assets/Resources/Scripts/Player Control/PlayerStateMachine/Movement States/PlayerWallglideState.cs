using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallglideState : PlayerMovementState
{
    public PlayerWallglideState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        Context.playerRb.useGravity = false;
        Context.animationController.SetBool("Airborne", true);
        Context.animationController.SetBool("Wallrunning", true);
        Context.inputContext.JumpDownEvent.AddListener(OnSpacebarDown);

        Context.wallRunning.IncomingMagnitude = Context.playerRb.velocity.XZMag();
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerRb.useGravity = true;
        Context.animationController.SetBool("Airborne", false);
        Context.animationController.SetBool("Wallrunning", false);
        Context.wallRunning.isWallRunning = false;
        Context.inputContext.JumpDownEvent.RemoveListener(OnSpacebarDown);
        Context.TPComponentController.RemoveKey(2);
    }

    public void OnSpacebarDown()
    {
        Context.wallRunning.JumpFromWall(Context.movementProfile.WallJumpSideVel, Context.movementProfile.WallJumpUpVel);
        Context.groundPhysicsContext.GroundedBlockTimer = Context.movementProfile.JumpGroundBlockDuration;
        TrySwitchState(Factory.Jump);
    }

    public override void UpdateState()
    {
        Context.wallRunning.DetectWalls();
        float tilt = Context.wallRunning.PlayerRightDotWallNormal > 0 ? 1 : 0;
        Context.TPComponentController.SetShoulderOffset(tilt,2);
        Context.animationController.SetFloat("RunTilt",tilt);
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {

    }

    public override void CheckSwitchState()
    {
        if (!Context.wallRunning.IsWallRunning())
        {
            Context.wallRunning.SetWallrunCooldown();
            TrySwitchState(Factory.Jump);
            return;
        }

        //Grounded check
        if (Context.groundPhysicsContext.IsGroundedForSteps(2))
        {
            TrySwitchState(Factory.Idle);
        }
    }
}
