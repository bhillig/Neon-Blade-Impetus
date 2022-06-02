using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMovementProfile")]
public class PlayerMovementProfile : ScriptableObject
{
    [Space(10)]
    [Header("Basic Movement")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float topMoveSpeed;
    [SerializeField] private float baseAcceleration;
    [SerializeField] private float baseFriction;
    [SerializeField] private AnimationCurve runAnimationSpeedCurve;

    [Space(10)]
    [Header("Momentum gain")]
    [SerializeField] private float overclockAcceleration;

    [Space(10)]
    [Header("Jump and Airborne")]
    [SerializeField] private float groundedToJumpDelay;
    [SerializeField] private float jumpHeight;
    [SerializeField, HideInInspector] private float jumpVelocity;
    [SerializeField] private float baseAirAcceleration;
    [SerializeField] private float airTurnSpeed;
    [SerializeField] private float jumpGroundBlockDuration;

    [Space(10)]
    [Header("Moment Preservation")]
    [SerializeField] private float runningPreservationRatio;
    [SerializeField] private float airbornePreservationRatio;

    [Space(10)]
    [Header("Landing Roll")]
    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollFallSpeedThreshhold;

    [Space(10)]
    [Header("Wallrun and Walljump")]
    [SerializeField] private float wallJumpUpHeight;
    [SerializeField] private float wallJumpUpVel;
    [SerializeField] private float wallJumpSideVel;

    [Space(10)]
    [Header("Grounded and Ground Snap")]
    [SerializeField] private float maxGroundedAngle;
    [SerializeField] private float maxSnapVelocity;
    [SerializeField] private AnimationCurve snapVelocityToAngleRatioCurve;
    // The player will snap over small ledges if the difference is less than this
    [SerializeField] private float maxSnapAngle;
    [SerializeField] private float snapProbeDistance;
    [SerializeField] private float maxAirRotationDot;

    [Space(10)]
    [Header("Slide")]
    [SerializeField] private float slideVelThreshhold;
    [SerializeField] private float slideGravityBoost;
    [SerializeField] private float slideSpeedBoostRatio;
    [SerializeField] private float slideBaseFriction;
    [SerializeField] private float slideLockDuration;
    [SerializeField] private float slideCooldown;
    [SerializeField] private float slideSpeedCap;

    [SerializeField,HideInInspector] private float minGroundedDotProd;

    public float TurnSpeed => turnSpeed;
    public float AirTurnSpeed => airTurnSpeed;
    public float BaseMoveSpeed => baseMoveSpeed;
    public float TopMoveSpeed => topMoveSpeed;
    public float BaseAcceleration => baseAcceleration;
    public AnimationCurve RunAnimationSpeedCurve => runAnimationSpeedCurve;
    public float OverclockAcceleration => overclockAcceleration;
    public float BaseAirAcceleration => baseAirAcceleration;
    public float BaseFriction => baseFriction;
    public float GroundedToJumpDelay => groundedToJumpDelay;
    public float JumpVelocity => jumpVelocity;
    public float JumpHeight => jumpHeight;
    public float JumpGroundBlockDuration => jumpGroundBlockDuration;
    public float RollSpeed => rollSpeed;
    public float RollFallSpeedThreshhold => rollFallSpeedThreshhold;
    public float WallJumpSideVel => wallJumpSideVel;
    public float WallJumpUpVel => wallJumpUpVel;
    public float RunningPreservationRatio => runningPreservationRatio;
    public float AirbornePreservationRatio => airbornePreservationRatio;


    public float MinGroundedDotProd => minGroundedDotProd;
    public float MinAirRotationDot => maxAirRotationDot;
    public float SlideVelThreshhold => slideVelThreshhold;
    public float SlideGravityBoost => slideGravityBoost;
    public float SlideBaseFriction => slideBaseFriction;
    public float SlideSpeedBoostRatio => slideSpeedBoostRatio;
    public float SlideLockDuration => slideLockDuration;
    public float SlideCooldown => slideCooldown;
    public float SlideSpeedCap => slideSpeedCap;
    public float GetMaxSnapDotProd(float vel)
    {
        float ratio = snapVelocityToAngleRatioCurve.Evaluate(vel / maxSnapVelocity);
        return ratio * Mathf.Sin(maxSnapAngle * Mathf.Deg2Rad);
    }
    public float SnapProbeDistance => snapProbeDistance;


    private void OnValidate()
    {
        UpdateCalculatedValues();
    }

    private void OnEnable()
    {
        UpdateCalculatedValues();
    }

    private void UpdateCalculatedValues()
    {
        // Calculate jump velocity based on jump height
        jumpVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
        wallJumpUpVel = Mathf.Sqrt(-2f * Physics.gravity.y * wallJumpUpHeight);
        minGroundedDotProd = Mathf.Cos(maxGroundedAngle * Mathf.Deg2Rad);
    }
}
