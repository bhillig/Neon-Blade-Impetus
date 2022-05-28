using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    // Dependencies
    public InputInfo inputContext;
    public PlayerMovementProfile movementProfile;
    public PlayerCombatProfile combatProfile;
    public ThirdPersonCameraTargetController cameraControlContext;
    public Animator animationController;
    public GroundedPhysicsContext groundPhysicsContext;
    public AirbornePhysicsContext airbornePhysicsContext;
    public Transform playerPhysicsTransform;
    public Transform playerModelTransform;
    public Transform playerCenter;
    public Rigidbody playerRb;
    public WallFinder wallFinder;
    public ColliderSwitcher colliderSwitcher;
    public ColliderEvents colliderEvents;
    public PlayerEventsAsset playerEvents;
    // State machine
    private PlayerStateMachine movementStateMachine;
    private PlayerStateMachine combatStateMachine;


    // Particle prefabs.
    [SerializeField]
    private GameObject runParticle;
    [SerializeField]
    private GameObject landParticle;
    [SerializeField]
    private GameObject slideParticle;

    // Particle getters.
    public GameObject RunParticle { get => runParticle; }
    public GameObject LandParticle { get => landParticle; }
    public GameObject SlideParticle { get => slideParticle; }

    // For particles usage in each state.
    private GameObject particle;
    private ParticleSystem ps;

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
