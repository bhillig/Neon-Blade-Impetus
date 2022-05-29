using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) {}

    private float timer = 0.0f;
    private float stateDuration = 0.70f;

    public override void EnterState()
    {
        Debug.Log("landed");
        Context.animationController.SetBool("Landing", true);
    }

    public override void ExitState()
    {
        timer = 0.0f;
        Debug.Log("no longer landed");
        Context.animationController.SetBool("Landing", false);
    }

    public override void UpdateState()
    {
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
}
