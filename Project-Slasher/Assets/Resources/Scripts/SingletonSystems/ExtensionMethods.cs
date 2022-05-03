using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static float AngleLerp(this float source, float target, float t)
    {
        return Mathf.Lerp(source, source + Mathf.DeltaAngle(source, target), t);
    }

    public static Vector3 ToXZPlane(this Vector2 vec)
    {
        return new Vector3(vec.x, 0, vec.y);
    }

    public static Vector2 RemoveY(this Vector3 vec)
    {
        vec.y = 0;
        return vec;
    }

    public static Quaternion ZeroXAxis(this Quaternion rot)
    {
        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, rot.eulerAngles.z);
        return rot;
    }

    public static void MoveWithRotation(this Rigidbody target, Quaternion rotation, Vector2 movementVector, float speed)
    {
        Vector3 moveVec = (
            rotation.ZeroXAxis() *
            movementVector.normalized.ToXZPlane() *
            speed);

        target.velocity = (new Vector3(moveVec.x, target.velocity.y, moveVec.z));
    }

    public static void RotateTowardsVelocity(this Transform transform, Rigidbody rb, float lerpVal, bool negate = false, float offset = 0)
    {
        float current = transform.eulerAngles.y;
        float target = Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.z, rb.velocity.x);
        if (negate)
            target *= -1;
        target += offset;
        float diff = Mathf.DeltaAngle(current, target);
        float newRot = Mathf.Lerp(current, current + diff, lerpVal);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newRot, transform.eulerAngles.z);
    }
}