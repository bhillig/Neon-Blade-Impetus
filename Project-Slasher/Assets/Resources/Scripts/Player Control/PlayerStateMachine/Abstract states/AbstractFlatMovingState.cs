using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used for "flat" movement like running/other movement on flat ground or normal falling mid-air
/// Uses the camera's Y axis rotation to rotate the input and whatnot
/// DO NOT use this for wall gliding, it ain't flat
/// If you do I will remove your kneecaps
/// </summary>
public abstract class AbstractFlatMovingState : PlayerBaseState
{
    protected AbstractFlatMovingState(PlayerStateMachine stateMachine, PlayerStateFactory factory) : base(stateMachine, factory)
    {
    }

    // State specific fields
    protected Vector2 movementInput;

    protected Vector3 GetDesiredVelocity(float maxSpeed)
    {
        // Calculate desired velocity
        movementInput = Context.inputContext.movementInput.normalized;
        Vector3 rotatedInput = Context.cameraControlContext.RotateAroundCameraY(movementInput.ToXZPlane());
        return maxSpeed * rotatedInput;
    }

    protected void SimpleMovement(Vector3 desiredVelocity, float maxAccelStep)
    {
        Vector3 velocityChange = MovePhysicsUtil.CalculateMovementAccelerationStep(
            Vector3.right,
            Vector3.forward,
            Context.groundPhysicsContext.ContactNormal,
            Context.playerRb.velocity,
            desiredVelocity,
            maxAccelStep);
        // Set value 
        Context.playerRb.velocity += velocityChange;
    }

    protected void LerpRotation(float factor)
    {
        LerpRotation(factor, movementInput);
    }

    protected void LerpRotation(float factor,Vector2 movementInput)
    {
        Vector3 normal = Context.groundPhysicsContext.RawNormal;
        // Why work in quaternions when you can do everything with vectors
        // This probably has terrible performance
        Vector3 forwardVector = 
            Quaternion.AngleAxis(-movementInput.Angle() + Context.cameraControlContext.rotation.eulerAngles.y + 90,Vector3.up) 
            * Vector3.forward;

        Vector3 rightTangent = Vector3.Cross(normal, -forwardVector);
        Vector3 biNormal = Vector3.Cross(normal, rightTangent);

        Quaternion targetRotation = Quaternion.LookRotation(biNormal, normal);
        Context.playerPhysicsTransform.rotation = Quaternion.Lerp(Context.playerPhysicsTransform.rotation, targetRotation, factor);
    }

}
