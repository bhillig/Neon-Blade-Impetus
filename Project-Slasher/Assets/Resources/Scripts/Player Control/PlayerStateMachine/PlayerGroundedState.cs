using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) {}

    public override void EnterState()
    {
        Context.inputContext.SpacebarDownEvent.AddListener(Jump);
        Context.inputContext.ShiftDownEvent.AddListener(Shift);
    }

    public override void ExitState()
    {
        Context.inputContext.SpacebarDownEvent.RemoveListener(Jump);
        Context.inputContext.ShiftDownEvent.RemoveListener(Shift);
    }

    protected virtual void Jump()
    {
        TrySwitchState(Factory.Jump);
        // Jump physics
        Vector3 vel = Context.playerRb.velocity;
        vel.y = Mathf.Max(0f, vel.y);
        Vector3 jumpVec = Context.groundPhysicsContext.ContactNormal * Context.movementProfile.JumpVelocity;
        vel += jumpVec;
        Context.playerRb.velocity = vel;
        Context.groundPhysicsContext.SnapToGroundBlock = 0.25f;
    }

    protected virtual void Shift()
    {
        TrySwitchState(Factory.Slide);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void FixedUpdateState()
    {

    }

    public override void CheckSwitchState()
    {
        if (!Context.groundPhysicsContext.IsGrounded())
        {
            TrySwitchState(Factory.Jump);
        }
    }
}
