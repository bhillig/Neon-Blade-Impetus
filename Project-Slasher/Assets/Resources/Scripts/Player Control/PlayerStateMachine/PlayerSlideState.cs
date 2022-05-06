using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : AbstractFlatMovingState
{
    public PlayerSlideState(PlayerStateMachine context, PlayerStateFactory factory) : base(context, factory) {}

    private float enterSpeedOffset;

    public override void EnterState()
    {
        Context.animationController.SetBool("Sliding", true);
        Context.inputContext.ShiftUpEvent.AddListener(Shift);
        enterSpeedOffset = (Context.movementProfile.SlideSpeedBoostRatio - 1) * Context.playerRb.velocity.magnitude;
        Vector3 cVel = Context.playerRb.velocity;
        cVel.x *= Context.movementProfile.SlideSpeedBoostRatio;
        cVel.z *= Context.movementProfile.SlideSpeedBoostRatio;
        Context.playerRb.velocity = cVel;
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Sliding", false);
        Context.inputContext.ShiftUpEvent.RemoveListener(Shift);
        Context.slideLock = Context.movementProfile.SlideLockDuration;
        Context.playerRb.velocity -= Context.playerRb.velocity.normalized * enterSpeedOffset;
    }

    private void Shift()
    {
        SwitchState(Factory.Run);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        Vector3 forwards = new Vector3(Context.playerRb.velocity.x, 0f, Context.playerRb.velocity.z).normalized;
        if(forwards != Vector3.zero)
            Context.forwardVector = forwards;
        LerpRotation(Context.movementProfile.TurnSpeed);
        Context.playerRb.AddForce(Vector3.down * Context.movementProfile.SlideGravityBoost);
        var rb = Context.playerRb;
        Vector3 cVel = rb.velocity;
        cVel = Vector3.MoveTowards(cVel, Vector3.zero, Context.movementProfile.SlideBaseFriction * Time.fixedDeltaTime);
        rb.velocity = cVel;
    }

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (Context.playerRb.velocity.magnitude < Context.movementProfile.SlideVelThreshhold)
            SwitchState(Factory.Run);
    }
}
