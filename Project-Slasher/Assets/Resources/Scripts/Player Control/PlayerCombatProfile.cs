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
    [SerializeField] private float velocity;
    [SerializeField] private float duration;

    public float ChargeTime => chargeTime;
    public float HoldTime => holdTime;
    public float Cooldown => cooldown;
    public float Velocity => velocity;
    public float Duration => duration;
}
