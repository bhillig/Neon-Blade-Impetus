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

    public float TurnSpeed => turnSpeed;
    public float BaseMoveSpeed => baseMoveSpeed;
    public float BaseAcceleration => baseAcceleration;
    public float BaseFriction => baseFriction;
}
