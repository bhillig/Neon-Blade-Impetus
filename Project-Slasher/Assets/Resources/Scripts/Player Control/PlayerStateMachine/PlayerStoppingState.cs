using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player is slowing down
/// </summary>
public class PlayerStoppingState : AbstractFlatMovingState
{
    public PlayerStoppingState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) {}

    private float frictionStep;

    public override void EnterState()
    {
        Context.animationController.SetBool("Running", true);
        frictionStep = Context.movementProfile.BaseFriction * Time.fixedDeltaTime;
        CheckSwitchStates();       
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.animationController.SetBool("Running", false);
    }

    public override void UpdateState()
    {
        Context.animationController.SetBool("Running", Context.playerRb.velocity.magnitude > frictionStep * 10);
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        //Friction simulation
        var rb = Context.playerRb;
        Vector3 cVel = rb.velocity;
        float sqrMag = cVel.XZSqrMag();
        if (sqrMag == 0)
            return;
        float xAxisRatio = cVel.x * cVel.x / sqrMag;
        float zAxisRatio = cVel.z * cVel.z / sqrMag;
        float yAxisRatio = cVel.z * cVel.z / sqrMag;
        cVel.x = Mathf.MoveTowards(cVel.x, 0f, xAxisRatio * frictionStep);
        cVel.z = Mathf.MoveTowards(cVel.z, 0f, zAxisRatio * frictionStep);
        cVel.y = Mathf.MoveTowards(cVel.y, 0f, yAxisRatio * frictionStep);
        rb.velocity = cVel;
        // Rotation
        LerpRotation(Context.movementProfile.TurnSpeed);
    }

    public override void InitializeSubState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if(Context.inputContext.movementInput != Vector2.zero)
            SwitchState(Factory.Run);
        else if(Context.playerRb.velocity.magnitude < 0.2f)
            SwitchState(Factory.Idle);
    }
}
