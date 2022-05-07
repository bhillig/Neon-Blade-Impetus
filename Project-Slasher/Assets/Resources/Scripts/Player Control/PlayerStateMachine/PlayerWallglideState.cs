using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallglideState : PlayerMovementState
{
    public PlayerWallglideState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    private WallSearchResult results;

    public override void EnterState()
    {
        results = Context.wallFinder.SearchForWall(Context.movementProfile.MinGroundedDotProd);
        Context.playerRb.useGravity = false;
        Context.animationController.SetBool("Airborne", true);
        Context.inputContext.SpacebarDownEvent.AddListener(OnSpacebarDown);
    }

    public override void ExitState()
    {
        Context.playerRb.useGravity = true;
        Context.animationController.SetBool("Airborne", false);
        Context.inputContext.SpacebarDownEvent.RemoveListener(OnSpacebarDown);
    }

    public void OnSpacebarDown()
    {
        TrySwitchState(Factory.Jump);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        results = Context.wallFinder.SearchForWall(Context.movementProfile.MinGroundedDotProd);
        if (results == null)
        {
            TrySwitchState(Factory.Jump);
            return;
        }
        Vector3 dir = Vector3.ProjectOnPlane(Camera.main.transform.forward, results.norm).normalized;
        Context.playerRb.velocity = dir * 25f;
        flatMove.LerpRotation(1f);
    }

    public override void CheckSwitchState()
    {
        //Grounded check
        if (Context.groundPhysicsContext.IsGrounded())
        {
            TrySwitchState(Factory.Idle);
        }
    }
}
