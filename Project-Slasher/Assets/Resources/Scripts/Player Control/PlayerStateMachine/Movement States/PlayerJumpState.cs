using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    public PlayerJumpState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) { }

    protected float acceleration;
    protected float maxSpeed;
    protected float maxSpeedChange;

    protected Vector3 desiredVelocity;

    private float hackZcache;

    public override void EnterState()
    {
        base.EnterState();
        // Grab some values from movementProfile
        acceleration = Context.movementProfile.BaseAirAcceleration;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;
        
        // Start particles.
        Context.Particle = GameObject.Instantiate(Context.JumpParticle, Context.transform, false);

        maxSpeed = Context.movementProfile.BaseMoveSpeed;
        // If going faster than movement profile's speed when entering state, then that becomes the new max speed
        //CalculateTopSpeed();
        hackZcache = Context.playerRb.velocity.z;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Context.playerRb.velocity = Context.playerRb.velocity.SetZ(hackZcache);
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        desiredVelocity = flatMove.GetDesiredVelocity(maxSpeed);
        if (desiredVelocity.magnitude > 0f)
        {
            flatMove.SimpleMovement(desiredVelocity, maxSpeedChange);
        }
        // Rotation
        // Only rotates X/Z to a threshhold
        if (Context.inputContext.movementInput != Vector2.zero)
            flatMove.UpdateFlatForwardVector(Context.inputContext.lastNZeroMovementInput);      
        if(Vector3.Dot(Context.playerPhysicsTransform.up,Vector3.up) <= Context.movementProfile.MinAirRotationDot)
            flatMove.LerpRotation(Context.movementProfile.AirTurnSpeed);
        else
            flatMove.LerpRotationY(Context.movementProfile.AirTurnSpeed);
        //UpdateTopSpeed();
        hackZcache = Context.playerRb.velocity.z;
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
    }
    private void UpdateTopSpeed()
    {
        float flatVel = Context.playerRb.velocity.XZMag();
        if (flatVel < maxSpeed)
        {
            maxSpeed = Mathf.LerpUnclamped(flatVel, maxSpeed, Context.movementProfile.AirbornePreservationRatio);
        }
        // Clamp to minimum speed
        maxSpeed = Mathf.Clamp(maxSpeed, Context.movementProfile.BaseMoveSpeed, Context.movementProfile.TopMoveSpeed);
    }
    private void CalculateTopSpeed()
    {
        float flatVel = Context.playerRb.velocity.XZMag();
        maxSpeed = flatVel;
        // Clamp to minimum speed
        maxSpeed = Mathf.Clamp(maxSpeed, Context.movementProfile.BaseMoveSpeed, Context.movementProfile.TopMoveSpeed);
    }
}
