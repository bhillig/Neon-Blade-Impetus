using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy HeadAI Profile")]
public class HeadAIProfile : ScriptableObject
{
    [Header("Targetting Values")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private float sightRange;
    [SerializeField] private float activationRange;
    [SerializeField] private float scopeRange;
    [SerializeField] private float reloadDelay;
    [SerializeField] private float alertTime;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float targetHeightOffset;

    [SerializeField] private float showdownRange;

    public LayerMask WhatIsPlayer { get => whatIsPlayer; set => whatIsPlayer = value; }
    public float SightRange { get => sightRange; set => sightRange = value; }
    public float ActivationRange { get => activationRange; set => activationRange = value; }
    public float ScopeRange { get => scopeRange; set => scopeRange = value; }
    public float ReloadDelay { get => reloadDelay; set => reloadDelay = value; }
    public float AlertTime { get => alertTime; set => alertTime = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float TargetHeightOffset { get => targetHeightOffset; set => targetHeightOffset = value; }

    public float ShowdownRange { get => showdownRange; set => showdownRange = value; }
}
