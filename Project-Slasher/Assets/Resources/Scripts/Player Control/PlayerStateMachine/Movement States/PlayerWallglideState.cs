using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallglideState : PlayerMovementState
{
    public PlayerWallglideState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) { }

    private float jumpLockout = 0f;
    public override void EnterState()
    {
        base.EnterState();
        Context.playerRb.useGravity = false;
        Context.playerRb.constraints |= RigidbodyConstraints.FreezePositionY;
        Context.animationController.SetBool("Airborne", true);
        Context.animationController.SetBool("Wallrunning", true);
        Context.inputContext.JumpDownEvent.AddListener(OnSpacebarDown);
        // Audio
        Context.playerEvents.FootstepTaken += PlayFootstepSound;
        Context.audioManager.defaultLandEmitter.Play();
        // particles
        // Start particles.
        Context.Particle = GameObject.Instantiate(Context.RunParticle, Context.transform, false);
        Context.Ps = Context.Particle.GetComponent<ParticleSystem>();
        // Jump lockout
        jumpLockout = 0.1f;
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.playerRb.useGravity = true;
        Context.playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        Context.animationController.SetBool("Airborne", false);
        Context.animationController.SetBool("Wallrunning", false);
        Context.animationController.speed = 1f;
        Context.wallRunning.isWallRunning = false;
        Context.StartCoroutine(CoroutExit());
        Context.inputContext.JumpDownEvent.RemoveListener(OnSpacebarDown);
        Context.playerEvents.FootstepTaken -= PlayFootstepSound;
        Context.Ps.Stop();
    }

    public override void UpdateState()
    {
        jumpLockout -= Time.deltaTime;
        Context.wallRunning.DetectWalls();
        float tilt = Context.wallRunning.PlayerRightDotWallNormal > 0 ? 1 : 0;
        if(tilt == 0)
        {
            Context.TPComponentController.SetLerpTimeMultiplier(1f);
            Context.TPComponentController.SetShoulderOffset(tilt);
        }
        Context.animationController.SetFloat("RunTilt",tilt);
        CheckSwitchState();
    }

    public override void FixedUpdateState()
    {
        flatMove.LerpRotation(
            Context.movementProfile.TurnSpeed*1.25f,
            Context.wallRunning.WallForward,
            Context.wallRunning.LastWallNormal * Context.wallRunning.Side);
        // Animation speed
        float horizontalSpeed = Vector3.ProjectOnPlane(Context.playerRb.velocity, Context.groundPhysicsContext.ContactNormal).magnitude;
        float speedRatio = Mathf.InverseLerp(0f, Context.movementProfile.TopMoveSpeed, horizontalSpeed);
        Context.animationController.speed = Context.movementProfile.RunAnimationSpeedCurve.Evaluate(speedRatio);
    }

    public override void CheckSwitchState()
    {
        if (!Context.wallRunning.IsWallRunning() && jumpLockout <= 0f)
        {
            // Running into an osbtacle while running will block you from wall running again for a longer duration
            if(Context.playerRb.velocity.magnitude < 5f)
                Context.wallRunning.SetWallrunCooldown(0.45f);
            else
                Context.wallRunning.SetWallrunCooldown(0.25f);
            TrySwitchState(Factory.Jump);
            return;
        }

        //Grounded check
        if (Context.groundPhysicsContext.IsGroundedForSteps(5))
        {
            TrySwitchState(Factory.Idle);
        }
    }
    public void OnSpacebarDown()
    {
        if (jumpLockout > 0f)
            return;
        Context.playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        Context.wallRunning.JumpFromWall(Context.movementProfile.WallJumpSideVel, Context.movementProfile.WallJumpUpVel);
        Context.groundPhysicsContext.GroundedBlockTimer = Context.movementProfile.JumpGroundBlockDuration;
        Context.audioManager.jumpEmitter.Play();
        // Start particles.
        Context.Particle = GameObject.Instantiate(Context.WallJumpParticle, Context.transform, false);
        TrySwitchState(Factory.Jump);
    }

    private IEnumerator CoroutExit()
    {
        yield return new WaitForSeconds(0.2f);
        Context.TPComponentController.SetShoulderOffset(1);
        Context.TPComponentController.SetLerpTimeMultiplier(3f);
    }

    private void PlayFootstepSound()
    {
        Context.audioManager.footStepEmitter.Play();
    }
}
