using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerMovementProfile")]
public class PlayerMovementProfile : ScriptableObject
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseAcceleration;
    [SerializeField] private float baseFriction;
    [SerializeField,HideInInspector] private float jumpVelocity;
    [SerializeField] private float jumpHeight;

    public float TurnSpeed => turnSpeed;
    public float BaseMoveSpeed => baseMoveSpeed;
    public float BaseAcceleration => baseAcceleration;
    public float BaseFriction => baseFriction;
    public float JumpVelocity => jumpVelocity;
    public float JumpHeight => jumpHeight;


    private void OnValidate()
    {
        // Calculate jump velocity based on jump height
        jumpVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
    }
}
