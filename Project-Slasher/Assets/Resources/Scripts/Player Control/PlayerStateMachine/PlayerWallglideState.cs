using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallglideState : AbstractFlatMovingState
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
        InitializeSubState();
        Context.inputContext.SpacebarDownEvent.AddListener(OnSpacebarDown);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerRb.useGravity = true;
        Context.animationController.SetBool("Airborne", false);
        Context.inputContext.SpacebarDownEvent.RemoveListener(OnSpacebarDown);
    }

    public void OnSpacebarDown()
    {
        SwitchState(Factory.Airborne);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        results = Context.wallFinder.SearchForWall(Context.movementProfile.MinGroundedDotProd);
        if (results == null)
        {
            SwitchState(Factory.Airborne);
            return;
        }
        Vector3 dir = Vector3.ProjectOnPlane(Camera.main.transform.forward, results.norm).normalized;
        Context.playerRb.velocity = dir * 25f;
        LerpRotation(1f);
    }

    public override void InitializeSubState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        //Grounded check
        if (Context.groundPhysicsContext.IsGrounded())
        {
            SwitchState(Factory.Grounded);
        }
    }
}
