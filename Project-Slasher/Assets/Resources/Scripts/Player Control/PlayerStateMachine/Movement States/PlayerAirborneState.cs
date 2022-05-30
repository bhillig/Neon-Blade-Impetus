using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAirborneState : PlayerMovementState
{
    public PlayerAirborneState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory)
    {
         
    }

    public override void EnterState()
    {
        base.EnterState();

        // Store initial height of the player.
        Context.ApexHeight = Context.transform.position.y;
        Context.colliderSwitcher.SwitchToCollider(3);
        Context.animationController.SetBool("Airborne", true);
        Context.inputContext.JumpDownEvent.AddListener(OnSpacebarDown);
    }

    public override void ExitState()
    {   
        base.ExitState();
        Context.colliderSwitcher.SwitchToCollider(0);

        Context.animationController.SetBool("Airborne", false);
        Context.inputContext.JumpDownEvent.RemoveListener(OnSpacebarDown);
    }

    public void OnSpacebarDown()
    {
/*        if(Context.wallFinder.SearchForWall(Context.movementProfile.MinGroundedDotProd) != null)
        {
            TrySwitchState(Factory.Wallglide);
        }*/
    }

    public override void UpdateState()
    {
        // Check to highest point of player when in the air.
        if(Context.transform.position.y > Context.ApexHeight)
            Context.ApexHeight = Context.transform.position.y;

        base.UpdateState();
    }

    private void LandingParticles()
    {
        // If the fall is geater than a certain initial, create large land particle.
        if (Context.transform.position.y < Context.ApexHeight - 7.0f)
        {
            // Start particles.
            Context.Particle = GameObject.Instantiate(Context.LargeLandParticle, Context.transform, false);
        }
        else if (Context.transform.position.y < Context.ApexHeight - 2.0f)
        {
            // Small Land particle
            Context.Particle = GameObject.Instantiate(Context.SmallLandParticle, Context.transform, false);
        }
    }

    public override void CheckSwitchState()
    {
        // wallrun check
        Context.wallRunning.DetectWalls(false);
        if (Context.wallRunning.ShouldWallRun(Context.mainCam.transform.forward) && Context.groundPhysicsContext.GroundedBlockTimer <= 0f)
        {
            Context.wallRunning.IncomingMagnitude = 
                Vector3.ProjectOnPlane(Context.playerRb.velocity.XZVec(), Context.wallRunning.LastWallNormal).magnitude;
            Context.wallRunning.DetectWalls(true);
            LandingParticles();
            TrySwitchState(Factory.Wallglide);
        }

        if(!Context.wallRunning.AboveGround(1.0f))
        {
            if (Context.playerRb.velocity.y <= -Context.movementProfile.RollFallSpeedThreshhold)
            {
                LandingParticles();
                TrySwitchState(Factory.Landing);
            }
        }

        //Grounded check
        if (Context.groundPhysicsContext.IsGrounded())
        {
            if (Context.playerRb.velocity.y <= -Context.movementProfile.RollFallSpeedThreshhold)
            {
                LandingParticles();
                TrySwitchState(Factory.Landing);
            }
            LandingParticles();
            TrySwitchState(Factory.GroundedSwitch);
        }
    }
}
