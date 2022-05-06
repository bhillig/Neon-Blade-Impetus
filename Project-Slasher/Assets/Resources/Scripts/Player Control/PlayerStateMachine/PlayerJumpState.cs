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

    public override void EnterState()
    {
        // Grab some values from movementProfile
        acceleration = Context.movementProfile.BaseAirAcceleration;
        maxSpeed = Context.movementProfile.BaseMoveSpeed;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        desiredVelocity = GetDesiredVelocity(maxSpeed);
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        if (desiredVelocity.magnitude > 0f)
            SimpleMovement(desiredVelocity,maxSpeedChange);
        // Rotation
        if (movementInput != Vector2.zero)
            UpdateFlatForwardVector(Context.inputContext.lastNZeroMovementInput);
        LerpRotation(Context.movementProfile.TurnSpeed);
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {

    }
}
