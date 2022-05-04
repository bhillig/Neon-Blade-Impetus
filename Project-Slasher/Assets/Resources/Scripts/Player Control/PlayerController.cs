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
    public ControlPhysicsbodyContext physicsbodyContext;
    public Transform playerPhysicsTransform;
    public Rigidbody playerRb;
    // State machine
    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        stateMachine.UpdateStateMachine();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdateStateMachine();
    }

    [ContextMenu("Print state tree")]
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
