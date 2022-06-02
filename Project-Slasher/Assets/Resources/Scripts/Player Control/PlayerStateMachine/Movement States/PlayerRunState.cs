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

    public override void EnterState()
    {
        base.EnterState();
        // Footsteps
        Context.playerEvents.FootstepTaken += PlayFootstepAudio;

        // Start particles.
        Context.Particle = GameObject.Instantiate(Context.RunParticle, Context.transform, false);
        Context.Ps = Context.Particle.GetComponent<ParticleSystem>();

        // Grab some values from movementProfile
        acceleration = Context.movementProfile.BaseAcceleration;
        maxSpeedChange = acceleration * Time.fixedDeltaTime;
        CalculateTopSpeed();
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Running", false);
        Context.animationController.speed = 1f;
        Context.playerEvents.FootstepTaken -= PlayFootstepAudio;
        // Stop Particles
        // They will be deleted from their own scrips after 1 second of stopped.
        Context.Ps.Stop();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Context.animationController.SetBool("Running", Context.playerRb.velocity.magnitude > 0.35f);
        desiredVelocity = flatMove.GetDesiredVelocity(maxSpeed);
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        float horizontalSpeed = Vector3.ProjectOnPlane(Context.playerRb.velocity, Context.groundPhysicsContext.ContactNormal).magnitude;
        // Overclock acceleration
        if (horizontalSpeed + maxSpeedChange >= maxSpeed && maxSpeed >= Context.movementProfile.BaseMoveSpeed)
        {
            maxSpeed += Time.fixedDeltaTime * Context.movementProfile.OverclockAcceleration;
            desiredVelocity = flatMove.GetDesiredVelocity(maxSpeed);
        }
        // Basic run movement
        if (desiredVelocity != Vector3.zero)
            flatMove.SimpleMovement(desiredVelocity,maxSpeedChange);

        // Rotation
        if (Context.inputContext.movementInput != Vector2.zero)
            flatMove.UpdateFlatForwardVector(Context.inputContext.lastNZeroMovementInput);
        flatMove.LerpRotation(Context.movementProfile.TurnSpeed);
        UpdateTopSpeed();
        // Animation speed
        float speedRatio = Mathf.InverseLerp(0f, Context.movementProfile.TopMoveSpeed, horizontalSpeed);
        Context.animationController.speed = Context.movementProfile.RunAnimationSpeedCurve.Evaluate(speedRatio);
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        if(Context.inputContext.movementInput == Vector2.zero)
        {
            TrySwitchState(Factory.Stopping);
        }
    }

    private void PlayFootstepAudio()
    {
        Context.audioManager.footStepEmitter.Play();
    }

    /// <summary>
    /// Always use the fastest top speed possible to preserve entry velocity 
    /// </summary>
    private void CalculateTopSpeed()
    {
        float flatVel = Vector3.ProjectOnPlane(Context.playerRb.velocity, Context.groundPhysicsContext.ContactNormal).magnitude;
        maxSpeed = flatVel;
        // Clamp to minimum speed
        maxSpeed = Mathf.Clamp(maxSpeed, Context.movementProfile.BaseMoveSpeed, Context.movementProfile.TopMoveSpeed);
    }
    private void UpdateTopSpeed()
    {
        float flatVel = Vector3.ProjectOnPlane(Context.playerRb.velocity, Context.groundPhysicsContext.ContactNormal).magnitude;
        if (flatVel < maxSpeed)
        {
            maxSpeed = Mathf.LerpUnclamped(flatVel, maxSpeed, Context.movementProfile.RunningPreservationRatio);
        }
        // Clamp to minimum speed
        maxSpeed = Mathf.Clamp(maxSpeed, Context.movementProfile.BaseMoveSpeed, Context.movementProfile.TopMoveSpeed);
    }
}
