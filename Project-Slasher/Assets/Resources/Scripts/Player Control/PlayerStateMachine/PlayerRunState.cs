using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) {}

    // State specific fields
    private Vector2 movementInput;
    private float rawInputMag;
    // Some movement fields for convenience
    private float acceleration;
    private float maxSpeed;
    private float maxSpeedChange;

    // Movement
    private Vector3 desiredVelocity;

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
        Context.animationController.SetBool("Running", false);
    }

    public override void UpdateState()
    {
        // Calculate desired velocity
        movementInput = Context.inputContext.movementInput.normalized;
        rawInputMag = Context.inputContext.movementInput.magnitude;
        Vector3 rotatedInput = Context.cameraControlContext.RotateAroundCameraY(movementInput.ToXZPlane());
        desiredVelocity = maxSpeed * rotatedInput;

        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        SimpleMovement();
        // Rotation
        if (movementInput != Vector2.zero)
        {
            Vector3 currentRotation = Context.playerPhysicsTransform.eulerAngles;
            float y = currentRotation.y.AngleLerp(-movementInput.Angle() + Context.cameraControlContext.rotation.eulerAngles.y + 90, Context.movementProfile.TurnSpeed);
            Context.playerPhysicsTransform.eulerAngles = new Vector3(currentRotation.x, y, currentRotation.z);
        }
    }

    private void SimpleMovement()
    {


        // Calculate new velocity
        var rb = Context.playerRb;
        Vector3 currentVel = rb.velocity;
        currentVel.x = Mathf.MoveTowards(currentVel.x, desiredVelocity.x, maxSpeedChange / rawInputMag);
        currentVel.z = Mathf.MoveTowards(currentVel.z, desiredVelocity.z, maxSpeedChange / rawInputMag);
        // Set value 
        rb.velocity = currentVel;
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
