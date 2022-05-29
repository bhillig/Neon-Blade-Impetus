using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) { }

    protected float acceleration;
    protected float maxSpeed;
    protected float maxSpeedChange;

    protected Vector3 desiredVelocity;

    public override void EnterState()
    {
        base.EnterState();

        // Start particles.
        Context.Particle = GameObject.Instantiate(Context.RunParticle, Context.transform, false);

        // Grab some values from movementProfile
        acceleration = Context.movementProfile.BaseAcceleration;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;
        CalculateTopSpeed();
    }

    /// <summary>
    /// Always use the fastest top speed possible to preserve entry velocity 
    /// </summary>
    private void CalculateTopSpeed()
    {
        float flatVel = Vector3.ProjectOnPlane(Context.playerRb.velocity, Context.groundPhysicsContext.ContactNormal).magnitude;
        if(flatVel < maxSpeed)
        {
            maxSpeed = Mathf.LerpUnclamped(flatVel, maxSpeed, Context.movementProfile.RunningPreservationRatio);
        }
        else
        {
            maxSpeed = flatVel;
        }
        // Clamp to minimum speed
        maxSpeed = Mathf.Clamp(maxSpeed,Context.movementProfile.BaseMoveSpeed,Context.movementProfile.TopMoveSpeed);
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Running", false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Context.animationController.SetBool("Running", Context.playerRb.velocity.magnitude > 0.35f);
        desiredVelocity = flatMove.GetDesiredVelocity(maxSpeed);
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        // Basic run movement
        if (desiredVelocity != Vector3.zero)
            flatMove.SimpleMovement(desiredVelocity,maxSpeedChange);

        // Rotation
        if (Context.inputContext.movementInput != Vector2.zero)
            flatMove.UpdateFlatForwardVector(Context.inputContext.lastNZeroMovementInput);
        flatMove.LerpRotation(Context.movementProfile.TurnSpeed);
        CalculateTopSpeed();
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if(Context.inputContext.movementInput == Vector2.zero)
        {
            TrySwitchState(Factory.Stopping);
        }
    }
}
