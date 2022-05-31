using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) {}

    private float timer = 0.0f;
    private float stateDuration = 0.52f;

    public override void EnterState()
    {
        base.EnterState();
        Context.animationController.SetBool("Landing", true);
        Context.colliderSwitcher.SwitchToCollider(1);
        // Adjust roll velocity
        Vector3 roll = Context.playerRb.velocity;
        Vector3 planeVel = Vector3.ProjectOnPlane(roll, Context.groundPhysicsContext.RawGroundNormal);
        float speed = planeVel.magnitude;
        Context.playerRb.velocity = 
            Vector3.up * roll.y + 
            Context.forwardVector.normalized *
            Mathf.Max(speed, Context.movementProfile.RollSpeed);
    }

    public override void ExitState()
    {
        base.ExitState();
        timer = 0.0f;
        Context.colliderSwitcher.SwitchToCollider(0);
        Context.animationController.SetBool("Landing", false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        timer += Time.deltaTime;
        if (timer >= stateDuration)
        {
            timer = 0.0f;
            TrySwitchState(Factory.GroundedSwitch);
        }

    }
    protected override void Jump()
    {
        // NO JUMPIN WHILE ROLLIN
    }

    protected override void Shift()
    {
        // NO SLIDIN EITHER
    }
}
