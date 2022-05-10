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

    public float ChargeTime => chargeTime;
    public float HoldTime => holdTime;
    public float Cooldown => cooldown;
}
