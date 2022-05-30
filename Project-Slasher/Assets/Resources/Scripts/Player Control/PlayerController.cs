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
    [Header("Scriptable Objects")]
    public PlayerMovementProfile movementProfile;
    public PlayerCombatProfile combatProfile;
    [Header("Misc")]
    public InputInfo inputContext;
    public Animator animationController;

    public PlayerEventsAsset playerEvents;
    // State machine
    private PlayerStateMachine movementStateMachine;
    private PlayerStateMachine combatStateMachine;

    // Player initialPosition for particle usage
    private float apexHeight;

    public float ApexHeight
    {
        get => apexHeight;
        set => apexHeight = value;
    }

    // Particle prefabs.
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

    // Particle getters.
    public GameObject RunParticle { get => runParticle; }
    public GameObject LargeLandParticle { get => largeLandParticle; }
    public GameObject SlideParticle { get => slideParticle; }
    public GameObject SmallLandParticle { get => smallLandParticle; }
    public GameObject JumpParticle { get => jumpParticle; }

    // For particles usage in each state.
    private GameObject particle;
    private ParticleSystem ps;
    
    // Particle for speed of player.
    private GameObject speedParticle;

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
    }

    private void Update()
    {
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
}
