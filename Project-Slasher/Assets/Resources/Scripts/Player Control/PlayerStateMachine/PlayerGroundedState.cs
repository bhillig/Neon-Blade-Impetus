using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) {}

    public override void EnterState()
    {
        InitializeSubState();
        Context.inputContext.SpacebarDownEvent.AddListener(Jump);
        Context.inputContext.ShiftDownEvent.AddListener(Shift);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.inputContext.SpacebarDownEvent.RemoveListener(Jump);
        Context.inputContext.ShiftDownEvent.RemoveListener(Shift);
    }

    private void Jump()
    {
        Vector3 vel = Context.playerRb.velocity;
        vel.y = Mathf.Max(0f, vel.y);
        Vector3 jumpVec = Context.groundPhysicsContext.ContactNormal * Context.movementProfile.JumpVelocity;
        vel += jumpVec;
        Context.playerRb.velocity = vel;
        SwitchState(Factory.Airborne);
        Context.groundPhysicsContext.SnapToGroundBlock = 0.5f;
    }

    private void Shift()
    {
        if (Context.playerRb.velocity.magnitude >= Context.movementProfile.SlideVelThreshhold)
            SwitchSubState(Factory.Slide);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {

    }

    public override void InitializeSubState()
    {
        if(Context.playerRb.velocity.magnitude == 0f)
            SwitchSubState(this.Factory.Idle);
        else if(Context.inputContext.shiftDown)
            SwitchSubState(this.Factory.Slide);
        else
            SwitchSubState(this.Factory.Stopping);
    }

    public override void CheckSwitchStates()
    {
        //Grounded check
        if (!Context.groundPhysicsContext.IsGrounded())
        {
            SwitchState(Factory.Airborne);
        }
    }
}
