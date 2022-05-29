using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used for "flat" movement like running/other movement on flat ground or normal falling mid-air
/// Uses the camera's Y axis rotation to rotate the input and whatnot
/// DO NOT use this for wall gliding, it ain't flat
/// If you do I will remove your kneecaps
/// </summary>
public class FlatMovingStateComponent
{
    public FlatMovingStateComponent(PlayerController context)
    {
        this.context = context;
    }

    private PlayerController context;

    // State specific fields
    protected Vector2 movementInput;

    public Vector3 GetDesiredVelocity(float maxSpeed)
    {
        // Calculate desired velocity
        movementInput = context.inputContext.movementInput.normalized;
        Vector3 rotatedInput = context.TPTargetController.RotateAroundCameraY(movementInput.ToXZPlane());
        return maxSpeed * rotatedInput;
    }

    public void SimpleMovement(Vector3 desiredVelocity, float maxAccelStep)
    {
        Vector3 velocityChange = MovePhysicsUtil.CalculateMovementAccelerationStep(
            Vector3.right,
            Vector3.forward,
            context.groundPhysicsContext.ContactNormal,
            context.playerRb.velocity,
            desiredVelocity,
            maxAccelStep);
        // Set value 
        context.playerRb.velocity += velocityChange;
    }

    public void UpdateFlatForwardVector(Vector2 input)
    {
        context.forwardVector =
            Quaternion.AngleAxis(-input.Angle() + context.TPTargetController.rotation.eulerAngles.y + 90, Vector3.up)
            * Vector3.forward;
    }
    
    public void LerpRotation(float factor)
    {
        Vector3 normal = context.groundPhysicsContext.RawGroundNormal;
        // Why work in quaternions when you can do everything with vectors
        // This probably has terrible performance

        Vector3 rightTangent = Vector3.Cross(normal, -context.forwardVector);
        Vector3 biNormal = Vector3.Cross(normal, rightTangent);
        Quaternion targetRotation = Quaternion.LookRotation(biNormal, normal);
        context.playerPhysicsTransform.rotation = Quaternion.Lerp(context.playerPhysicsTransform.rotation, targetRotation, factor);
    }

    public void LerpRotationY(float factor)
    {
        Vector3 normal = context.playerPhysicsTransform.up;
        // Why work in quaternions when you can do everything with vectors
        // This probably has terrible performance

        Vector3 rightTangent = Vector3.Cross(normal, -context.forwardVector);
        Vector3 biNormal = Vector3.Cross(normal, rightTangent);

        Quaternion targetRotation = Quaternion.LookRotation(biNormal, normal);
        context.playerPhysicsTransform.rotation = Quaternion.Lerp(context.playerPhysicsTransform.rotation, targetRotation, factor);
    }

}
