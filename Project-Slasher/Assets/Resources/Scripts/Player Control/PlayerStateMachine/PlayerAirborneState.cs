using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    public override void EnterState()
    {
        Context.animationController.SetBool("Airborne", true);
        Context.inputContext.SpacebarDownEvent.AddListener(OnSpacebarDown);
    }

    public override void ExitState()
    {
        Context.animationController.SetBool("Airborne", false);
        Context.inputContext.SpacebarDownEvent.RemoveListener(OnSpacebarDown);
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
        //Grounded check
        if (Context.groundPhysicsContext.IsGrounded() && Context.groundPhysicsContext.SnapToGroundBlock <= 0)
        {
            TrySwitchState(Factory.GroundedSwitch);
        }
    }
}
