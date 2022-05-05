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

    public static void DrawNormal(this RaycastHit hit)
    {
        Debug.DrawRay(hit.point, hit.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
    }

    public static Quaternion ZeroXAxis(this Quaternion rot)
    {
        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, rot.eulerAngles.z);
        return rot;
    }

    public static float Angle(this Vector2 vec)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(vec.y, vec.x);
    }

    public static float XZSqrMag(this Vector3 vec)
    {
        return new Vector2(vec.x, vec.z).SqrMagnitude();
    }

    /// <summary>
    /// Rotate a transform towards the velocity of a rb
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="rb"></param>
    /// <param name="lerpVal"></param>
    /// <param name="negate"></param>
    /// <param name="offset"></param>
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

    public static bool IsInLayerMask(this GameObject gameObject, LayerMask mask)
    {
        return ((mask.value) & (1 << gameObject.layer)) == (1 << gameObject.layer);
    }
}