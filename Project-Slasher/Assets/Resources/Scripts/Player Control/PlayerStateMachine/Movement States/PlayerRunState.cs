using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) { }

    protected float acceleration;
    protected float maxSpeed;
    protected float maxSpeedChange;

    protected Vector3 desiredVelocity;

    protected GameObject particle;
    protected ParticleSystem ps;
    public override void EnterState()
    {
        base.EnterState();
        Context.animationController.SetBool("Running", true);
        particle = GameObject.Instantiate(Context.RunParticle, Context.transform, false);
        ps = particle.GetComponent<ParticleSystem>();
        // Grab some values from movementProfile
        acceleration = Context.movementProfile.BaseAcceleration;
        maxSpeed = Context.movementProfile.BaseMoveSpeed;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;
    }

    public override void ExitState()
    {
        base.ExitState();
        ps.Stop();
        //GameObject.Destroy(particle);
        Context.animationController.SetBool("Running", false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        desiredVelocity = flatMove.GetDesiredVelocity(maxSpeed);
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        // Basic run movement
        if (desiredVelocity != Vector3.zero)
            flatMove.SimpleMovement(desiredVelocity,maxSpeedChange);

        // Rotation
        if (Context.inputContext.movementInput != Vector2.zero)
            flatMove.UpdateFlatForwardVector(Context.inputContext.lastNZeroMovementInput);
        flatMove.LerpRotation(Context.movementProfile.TurnSpeed);
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if(Context.inputContext.movementInput == Vector2.zero)
        {
            TrySwitchState(Factory.Stopping);
        }
    }
}
