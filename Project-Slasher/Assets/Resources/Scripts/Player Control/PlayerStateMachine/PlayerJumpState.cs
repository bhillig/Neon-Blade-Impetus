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
    private float maxVel;

    public override void EnterState()
    {
        base.EnterState();
        // Grab some values from movementProfile
        acceleration = Context.movementProfile.BaseAirAcceleration;
        maxSpeed = Context.movementProfile.BaseMoveSpeed;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;

        // If going faster than movement profile's speed when entering state, then that becomes the new max speed
        CompareEnterSpeed();
    }

    private void CompareEnterSpeed()
    {
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
        base.UpdateState();
        desiredVelocity = flatMove.GetDesiredVelocity(maxVel);
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        if (desiredVelocity.magnitude > 0f)
        {           
            flatMove.SimpleMovement(desiredVelocity,maxSpeedChange);
        }
        // Rotation
        // Only rotates to a threshhold for aesthetic reasons
        if (Context.inputContext.movementInput != Vector2.zero)
            flatMove.UpdateFlatForwardVector(Context.inputContext.lastNZeroMovementInput);      
        if(Vector3.Dot(Context.playerPhysicsTransform.up,Vector3.up) <= Context.movementProfile.MinAirRotationDot)
            flatMove.LerpRotation(Context.movementProfile.AirTurnSpeed);
        else
            flatMove.LerpRotationY(Context.movementProfile.AirTurnSpeed);
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
    }
}
