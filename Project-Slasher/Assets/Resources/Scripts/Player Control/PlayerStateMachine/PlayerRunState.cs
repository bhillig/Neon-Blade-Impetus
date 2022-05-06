using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : AbstractFlatMovingState
{
    public PlayerRunState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) {}

    protected float acceleration;
    protected float maxSpeed;
    protected float maxSpeedChange;

    protected Vector3 desiredVelocity;

    public override void EnterState()
    {
        Context.animationController.SetBool("Running", true);
        // Grab some values from movementProfile
        acceleration = Context.movementProfile.BaseAcceleration;
        maxSpeed = Context.movementProfile.BaseMoveSpeed;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Running", false);
    }

    public override void UpdateState()
    {
        desiredVelocity = GetDesiredVelocity(maxSpeed);
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        if (desiredVelocity != Vector3.zero)
            SimpleMovement(desiredVelocity,maxSpeedChange);
        // Rotation
        if (movementInput != Vector2.zero)
            UpdateFlatForwardVector(Context.inputContext.lastNZeroMovementInput);
        LerpRotation(Context.movementProfile.TurnSpeed);
        Context.groundPhysicsContext.DisplayGroundVectors();
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if(movementInput == Vector2.zero)
        {
            SwitchState(Factory.Stopping);
        }
    }
}
