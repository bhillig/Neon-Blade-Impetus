using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// <para>Util class that deals with basic vector operations for movement</para>
/// <para>If I have to work in 3d math again I'm going to take a bath with a toaster</para>
/// <para>Send help</para>
/// </summary>
public static class MovePhysicsUtil
{

    /// <summary>
    /// <para>Calculates the change in velocity given a number of parameters</para>
    /// <para>Assumes relative horizontal movement(movement on one plane)</para>
    /// <para>Desired velocity is used relative to the plane, note that y axis is not used</para>
    /// </summary>
    /// <param name="baseXAxis"></param>
    /// <param name="baseZAxis"></param>
    /// <param name="planeNormal"></param>
    /// <param name="currentVelocity"></param>
    /// <param name="desiredVelocity"></param>
    /// <param name="maxAccelStep"></param>
    /// <returns></returns>
    public static Vector3 CalculateMovementAccelerationStep(
        Vector3 baseXAxis,
        Vector3 baseZAxis,
        Vector3 planeNormal,
        Vector3 currentVelocity,
        Vector3 desiredVelocity, 
        float maxAccelStep
        )
    {
        // Project the axis we want to move along onto the plane
        Vector3 xAxisProjected = Vector3.ProjectOnPlane(baseXAxis, planeNormal).normalized;
        Vector3 zAxisProjected = Vector3.ProjectOnPlane(baseZAxis, planeNormal).normalized;

        // Calculate the current velocity along these projected Axis
        float currentX = Vector3.Dot(currentVelocity, xAxisProjected);
        float currentZ = Vector3.Dot(currentVelocity, zAxisProjected);
        
        // Use maxAccelStep to move the current velocities on these axis towards the desired values
        float newX =
            Mathf.MoveTowards(currentX, desiredVelocity.x, maxAccelStep);
        float newZ =
            Mathf.MoveTowards(currentZ, desiredVelocity.z, maxAccelStep);

        // Return the delta
        return xAxisProjected * (newX - currentX) + zAxisProjected * (newZ - currentZ);
    }

}
