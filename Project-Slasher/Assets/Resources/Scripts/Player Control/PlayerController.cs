using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Dependencies
    public InputInfo inputContext;
    public PlayerMovementProfile movementProfile;
    public ThirdPersonCameraTargetController cameraControlContext;
    public Animator animationController;
    public GroundedPhysicsContext groundPhysicsContext;
    public Transform playerPhysicsTransform;
    public Rigidbody playerRb;
    public WallFinder wallFinder;
    // State machine
    private PlayerStateMachine stateMachine;

    // Some context scope values
    [HideInInspector] public Vector3 forwardVector;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
        forwardVector = Vector3.forward;
    }

    private void Update()
    {
        stateMachine.UpdateStateMachine();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdateStateMachine();
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
        PlayerBaseState state = (PlayerBaseState)stateMachine.CurrentState;
        print(state);
        while(true)
        {
            state = (PlayerBaseState)state.CurrentSubState;
            if (state == null)
                return;
            print(state);
        }
    }
}
