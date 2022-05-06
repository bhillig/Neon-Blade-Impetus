using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
        
    }

    public override void EnterState()
    {
        Context.animationController.SetBool("Airborne", true);
        InitializeSubState();
        Context.inputContext.SpacebarDownEvent.AddListener(OnSpacebarDown);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Airborne", false);
        Context.inputContext.SpacebarDownEvent.RemoveListener(OnSpacebarDown);
    }

    public void OnSpacebarDown()
    {
        if(Context.wallFinder.SearchForWall(Context.movementProfile.MinGroundedDotProd) != null)
        {
            SwitchState(Factory.Wallglide);
        }
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void InitializeSubState()
    {
        SwitchSubState(Factory.Jump);
    }

    public override void CheckSwitchStates()
    {
        //Grounded check
        if (Context.groundPhysicsContext.IsGrounded() && Context.groundPhysicsContext.SnapToGroundBlock <= 0)
        {
            SwitchState(Factory.Grounded);
        }
    }
}
