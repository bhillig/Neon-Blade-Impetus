using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerCombatProfile")]
public class PlayerCombatProfile : ScriptableObject
{
    // Primary execute attack fields
    [SerializeField] private float chargeTime;
    [SerializeField] private float holdTime;
    [SerializeField] private float cooldown;
    [SerializeField] private float castRadius;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private LayerMask targetMask;
    [Space(15f)]
    [SerializeField] private float hitVelocity;
    [SerializeField] private float hitBoostVelocity;
    [SerializeField] private float hitDashPierceDistance;
    [Space(15f)]
    [SerializeField] private float dryVelocity;
    [SerializeField] private float dryExitVelocity;
    [SerializeField] private float dryDashDistance;

    public float ChargeTime => chargeTime;
    public float HoldTime => holdTime;
    public float Cooldown => cooldown;
    public float CastRadius => castRadius;
    public float CastDistance => castDistance;
    public LayerMask HitMask => hitMask;
    public LayerMask TargetMask => targetMask;

    public float HitVelocity => hitVelocity;
    public float HitBoostVelocity => hitBoostVelocity;
    public float HitDashPierceDistance => hitDashPierceDistance;

    public float DryVelocity => dryVelocity;
    public float DryExitVelocity => dryExitVelocity;
    public float DryDashDistance => dryDashDistance;
}
