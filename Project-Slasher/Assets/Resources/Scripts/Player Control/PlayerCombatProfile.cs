using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerCombatProfile")]
public class PlayerCombatProfile : ScriptableObject
{
    [Header("Charge Fields")]
    [SerializeField] private float chargeTime;
    [SerializeField] private float holdTime;
    [SerializeField] private float cooldown;

    [Header("Targeted Strike Lock-on")]
    [SerializeField] private float castRadius;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private LayerMask targetMask;

    [Header("Targeted strike physics")]
    [SerializeField] private float hitVelocity;
    [SerializeField] private float hitBoostVelocity;
    [SerializeField] private float hitDashPierceDistance;

    [Header("Time Slow")]
    [SerializeField] private float timeSlowScale;
    [SerializeField] private float timeSlowDuration;
    [SerializeField] private float timeSlowStartOffset;

    [Header("Dry strike physics")]
    [SerializeField] private float dryVelocity;
    [SerializeField] private float dryExitVelocity;
    [SerializeField] private float dryDashDistance;

    public float ChargeTime { get => chargeTime; set => chargeTime = value; }
    public float HoldTime { get => holdTime; set => holdTime = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float CastRadius { get => castRadius; set => castRadius = value; }
    public float CastDistance { get => castDistance; set => castDistance = value; }
    public LayerMask HitMask { get => hitMask; set => hitMask = value; }
    public LayerMask TargetMask { get => targetMask; set => targetMask = value; }
    public float HitVelocity { get => hitVelocity; set => hitVelocity = value; }
    public float HitBoostVelocity { get => hitBoostVelocity; set => hitBoostVelocity = value; }
    public float HitDashPierceDistance { get => hitDashPierceDistance; set => hitDashPierceDistance = value; }
    public float TimeSlowScale { get => timeSlowScale; set => timeSlowScale = value; }
    public float TimeSlowDuration { get => timeSlowDuration; set => timeSlowDuration = value; }
    public float TimeSlowStartOffset { get => timeSlowStartOffset; set => timeSlowStartOffset = value; }
    public float DryVelocity { get => dryVelocity; set => dryVelocity = value; }
    public float DryExitVelocity { get => dryExitVelocity; set => dryExitVelocity = value; }
    public float DryDashDistance { get => dryDashDistance; set => dryDashDistance = value; }
}
