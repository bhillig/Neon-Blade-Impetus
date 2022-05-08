using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerGroundedState
{
    public PlayerSlideState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) {}

    private float slideLockTimer;

    public override void EnterState()
    {
        base.EnterState();
        Context.animationController.SetBool("Sliding", true);
        Context.colliderSwitcher.SwitchToCollider(1);
        SlideEnterPhysics();
        // Slide timers
        Context.slideCooldownTimer = Context.movementProfile.SlideCooldown;
        slideLockTimer = Context.movementProfile.SlideLockDuration;
    }

    private void SlideEnterPhysics()
    {
        Vector3 cVel = Context.playerRb.velocity;
        cVel.x *= Context.movementProfile.SlideSpeedBoostRatio;
        cVel.z *= Context.movementProfile.SlideSpeedBoostRatio;
        Context.playerRb.velocity = cVel;
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Sliding", false);
        Context.colliderSwitcher.SwitchToCollider(0);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // Slide timers update
        Context.slideCooldownTimer = Context.movementProfile.SlideCooldown;
        slideLockTimer -= Time.deltaTime;
        CheckSwitchState();
    }

    public override bool IsStateSwitchable()
    {
        return (Context.playerRb.velocity.magnitude >= Context.movementProfile.SlideVelThreshhold) &&
            (Context.slideCooldownTimer <= 0f);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        RotateTowardsSlide();
        // Gravity is amplified when sliding
        Context.playerRb.AddForce(Vector3.down * Context.movementProfile.SlideGravityBoost);
        ApplySlideFriction();
    }

    private void RotateTowardsSlide()
    {
        Vector3 forwards = new Vector3(Context.playerRb.velocity.x, 0f, Context.playerRb.velocity.z).normalized;
        if (forwards != Vector3.zero)
            Context.forwardVector = forwards;
        flatMove.LerpRotation(Context.movementProfile.TurnSpeed);
    }

    private void ApplySlideFriction()
    {
        var rb = Context.playerRb;
        Vector3 cVel = rb.velocity;
        cVel = Vector3.MoveTowards(
            cVel, 
            Vector3.zero, 
            Context.movementProfile.SlideBaseFriction * Time.fixedDeltaTime);
        rb.velocity = cVel;
    }

    // Override GroundedState's jump logic to include a slideLockTimer check
    protected override void Jump()
    {
        if(slideLockTimer <= 0f)
            base.Jump();
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();
        bool userCancel = !Context.inputContext.shiftDown;
        bool tooSlow = Context.playerRb.velocity.magnitude < Context.movementProfile.SlideVelThreshhold;
        if ((userCancel || tooSlow) && slideLockTimer <= 0f)
            TrySwitchState(Factory.GroundedSwitch);
    }
}
