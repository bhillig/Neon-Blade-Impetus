using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : AbstractFlatMovingState
{
    public PlayerJumpState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory)
    {

    }

    protected float acceleration;
    protected float maxSpeed;
    protected float maxSpeedChange;

    protected Vector3 desiredVelocity;
    private float maxVel;

    public override void EnterState()
    {
        // Grab some values from movementProfile
        acceleration = Context.movementProfile.BaseAirAcceleration;
        maxSpeed = Context.movementProfile.BaseMoveSpeed;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;

        // If going faster than top speed when entering, then that becomes the new max speed
        float mag = Context.playerRb.velocity.XZMag();
        float desiredMag = desiredVelocity.magnitude;
        maxVel = maxSpeed;
        if (desiredMag < mag)
            maxVel = mag;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        desiredVelocity = GetDesiredVelocity(maxVel);
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        if (desiredVelocity.magnitude > 0f)
        {           
            SimpleMovement(desiredVelocity,maxSpeedChange);
        }
        // Rotation
        if (movementInput != Vector2.zero)
            UpdateFlatForwardVector(Context.inputContext.lastNZeroMovementInput);
        if(Vector3.Dot(Context.playerPhysicsTransform.up,Vector3.up) <= Context.movementProfile.MinAirRotationDot)
            LerpRotation(Context.movementProfile.AirTurnSpeed);
        else
            LerpRotationY(Context.movementProfile.AirTurnSpeed);
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {

    }
}
