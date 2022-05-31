using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    // Dependencies
    [Header("Transforms")]
    public Transform playerPhysicsTransform;
    public Transform playerModelTransform;
    public Transform playerCenter;

    [Header("Camera")]
    public Camera mainCam;
    public ThirdPersonCameraTargetController TPTargetController;
    public CinemachineTPFollowController TPComponentController;

    [Header("Movement Scripts")]
    public GroundedPhysicsContext groundPhysicsContext;
    public AirbornePhysicsContext airbornePhysicsContext;
    public WallFinder wallFinder;
    public Wallrunning wallRunning;

    [Header("Physics Dependencies")]
    public ColliderSwitcher colliderSwitcher;
    public ColliderEvents colliderEvents;
    public Rigidbody playerRb;
    public Cloth scarfCloth;

    [Header("Scriptable Objects")]
    public PlayerMovementProfile movementProfile;
    public PlayerCombatProfile combatProfile;

    [Header("Misc")]
    public InputInfo inputContext;
    public Animator animationController;

    [Header("Particles")]
    [SerializeField]
    private GameObject runParticle;
    [SerializeField]
    private GameObject largeLandParticle;
    [SerializeField]
    private GameObject slideParticle;
    [SerializeField]
    private GameObject smallLandParticle;
    [SerializeField]
    private GameObject jumpParticle;
    [SerializeField]
    private GameObject speedParticle;
    [SerializeField]
    private ParticleSystem deathParticles;
    // Particle getters.
    public GameObject RunParticle { get => runParticle; }
    public GameObject LargeLandParticle { get => largeLandParticle; }
    public GameObject SlideParticle { get => slideParticle; }
    public GameObject SmallLandParticle { get => smallLandParticle; }
    public GameObject JumpParticle { get => jumpParticle; }
    public ParticleSystem DeathParticles { get => deathParticles; }

    public PlayerEventsAsset playerEvents;
    // State machine
    private PlayerStateMachine movementStateMachine;
    private PlayerStateMachine combatStateMachine;

    // Player Death
    private Vector3 respawnPoint;

    public Vector3 RespawnPoint
    {
        get => respawnPoint;
        set => respawnPoint = value;
    }

    // Player initialPosition for particle usage
    private float apexHeight;

    public float ApexHeight
    {
        get => apexHeight;
        set => apexHeight = value;
    }

    // For particles usage in each state.
    private GameObject particle;
    private ParticleSystem ps;

    // ParticleSystem for speed of player.
    private ParticleSystem speedPs;
    public ParticleSystem SpeedPs => speedPs;

    // Particle usage getters.
    public GameObject Particle
    {
        get => particle;
        set => particle = value;
    }
    public ParticleSystem Ps
    {
        get => ps;
        set => ps = value;
    }
    // Some context scope values
    [HideInInspector] public Vector3 forwardVector;
    [HideInInspector] public float slideCooldownTimer;
    [HideInInspector] public float primaryAttackCooldownTimer;
    [HideInInspector] public Collider combatTarget;

    private void Awake()
    {
        movementStateMachine = new PlayerMovementStateMachine(this);
        combatStateMachine = new PlayerCombatStateMachine(this);
        wallRunning = new Wallrunning(this);
        mainCam = Camera.main;
        forwardVector = Vector3.forward;
        respawnPoint = transform.position;

        particle = Instantiate(speedParticle, gameObject.transform, false);
        speedPs = particle.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        SetSpeedParticleEmission();

        combatStateMachine.UpdateStateMachine();
        movementStateMachine.UpdateStateMachine();
    }

    private void FixedUpdate()
    {
        combatStateMachine.FixedUpdateStateMachine();
        movementStateMachine.FixedUpdateStateMachine();
        if (shouldConstPrintState)
            PrintState();
    }

    private bool shouldConstPrintState = false;

    [ContextMenu("Toggle Print state tree")]
    public void toggle()
    {
        shouldConstPrintState = !shouldConstPrintState;
    }
    public void PrintState()
    {
        PlayerBaseState state = (PlayerBaseState)movementStateMachine.CurrentState;
        print(state);
    }

    // Change the speed particle emission depending on the speed of the player.
    private void SetSpeedParticleEmission()
    {
        var emission = speedPs.emission;
        var velOverLifetime = speedPs.velocityOverLifetime;
        emission.rateOverTime = Mathf.Pow(playerRb.velocity.magnitude / 5f, 2.5f);
        velOverLifetime.z = Mathf.Sqrt(playerRb.velocity.magnitude) * 2f;
    }
}
