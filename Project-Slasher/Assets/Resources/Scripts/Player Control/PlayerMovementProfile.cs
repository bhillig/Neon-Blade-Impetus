using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMovementProfile")]
public class PlayerMovementProfile : ScriptableObject
{
    // Basic movement fields
    [SerializeField] private float turnSpeed;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseAcceleration;
    [SerializeField] private float baseAirAcceleration;
    [SerializeField] private float baseFriction;
    [SerializeField,HideInInspector] private float jumpVelocity;
    [SerializeField] private float jumpHeight;

    // State calculation fields
    [Space(15f)]
    [SerializeField] private float maxGroundedAngle;
    // The player will snap over small ledges if the difference is less than this
    [SerializeField] private float maxSnapAngle;
    [SerializeField] private float snapProbeDistance;

    [SerializeField, HideInInspector] private float minGroundedDotProd;
    [SerializeField, HideInInspector] private float maxSnapDotProd;

    public float TurnSpeed => turnSpeed;
    public float BaseMoveSpeed => baseMoveSpeed;
    public float BaseAcceleration => baseAcceleration;
    public float BaseAirAcceleration => baseAirAcceleration;
    public float BaseFriction => baseFriction;
    public float JumpVelocity => jumpVelocity;
    public float JumpHeight => jumpHeight;

    public float MinGroundedDotProd => minGroundedDotProd;
    public float MaxSnapDotProd => maxSnapDotProd;
    public float SnapProbeDistance => snapProbeDistance;


    private void OnValidate()
    {
        // Calculate jump velocity based on jump height
        jumpVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
        minGroundedDotProd = Mathf.Cos(maxGroundedAngle * Mathf.Deg2Rad);
        maxSnapDotProd = Mathf.Sin(maxSnapAngle * Mathf.Deg2Rad);
    }
}
