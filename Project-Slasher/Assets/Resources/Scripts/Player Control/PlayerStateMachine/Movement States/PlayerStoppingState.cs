using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player is slowing down
/// </summary>
public class PlayerStoppingState : PlayerGroundedState
{
    public PlayerStoppingState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) {}

    private float frictionStep;

    public override void EnterState()
    {
        base.EnterState();
        Context.animationController.SetBool("Running", true);
        frictionStep = Context.movementProfile.BaseFriction * Time.fixedDeltaTime;
        CheckSwitchState();       
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerRb.useGravity = true;
        Context.animationController.SetBool("Running", false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Context.animationController.SetBool("Running", Context.playerRb.velocity.magnitude > frictionStep * 10);
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        Context.playerRb.useGravity = !Context.groundPhysicsContext.IsGroundedRaw();
        // Apply friction
        var rb = Context.playerRb;
        Vector3 cVel = rb.velocity;
        cVel = Vector3.MoveTowards(cVel, Vector3.zero, Context.movementProfile.BaseFriction * Time.fixedDeltaTime);
        rb.velocity = cVel;
        // Rotation
        flatMove.LerpRotation(Context.movementProfile.TurnSpeed);
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if(Context.inputContext.movementInput != Vector2.zero)
            TrySwitchState(Factory.Run);
        else if(Context.playerRb.velocity.magnitude < 0.2f)
            TrySwitchState(Factory.Idle);
    }
}
