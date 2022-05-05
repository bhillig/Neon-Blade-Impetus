using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractMovingState : PlayerBaseState
{
    protected AbstractMovingState(PlayerStateMachine stateMachine, PlayerStateFactory factory) : base(stateMachine, factory)
    {
    }

    // State specific fields
    protected Vector2 movementInput;
    protected float rawInputMag;

    protected Vector3 GetDesiredVelocity(float maxSpeed)
    {
        // Calculate desired velocity
        movementInput = Context.inputContext.movementInput.normalized;
        rawInputMag = Context.inputContext.movementInput.magnitude;
        Vector3 rotatedInput = Context.cameraControlContext.RotateAroundCameraY(movementInput.ToXZPlane());
        //Project onto the plane
        return maxSpeed * rotatedInput;
    }

    protected Vector3 GetDesiredVelocityOnPlane(float maxSpeed)
    {
        return maxSpeed * Context.physicsbodyContext.ProjectOnContactPlane(GetDesiredVelocity(maxSpeed)).normalized;
    }

    protected void SimpleMovement(Vector3 desiredVelocity, float maxAccelStep)
    {
        Vector3 xAxis = Context.physicsbodyContext.ProjectOnContactPlane(Vector3.right).normalized;
        Vector3 zAxis = Context.physicsbodyContext.ProjectOnContactPlane(Vector3.forward).normalized;

        float currentX = Vector3.Dot(Context.playerRb.velocity, xAxis);
        float currentZ = Vector3.Dot(Context.playerRb.velocity, zAxis);
        // Calculate new velocity
        var rb = Context.playerRb;
        Vector3 currentVel = rb.velocity;

        float newX =
            Mathf.MoveTowards(currentX, desiredVelocity.x, maxAccelStep);
        float newZ =
            Mathf.MoveTowards(currentZ, desiredVelocity.z, maxAccelStep);

        currentVel += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
        // Set value 
        rb.velocity = currentVel;
    }

    protected void LerpRotation(float factor)
    {
        Vector3 currentRotation = Context.playerPhysicsTransform.eulerAngles;
        float y = currentRotation.y.AngleLerp(-movementInput.Angle() + Context.cameraControlContext.rotation.eulerAngles.y + 90, factor);
        Context.playerPhysicsTransform.eulerAngles = new Vector3(currentRotation.x, y, currentRotation.z);
    }

}
