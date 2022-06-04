using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatDeadState : PlayerCombatState
{
    public PlayerCombatDeadState(PlayerStateMachine context, PlayerStateFactory factory) : base(context,factory) { }

    private Vector3 scarfRandAccel;
    private CapsuleCollider[] scarfColliders;

    public override void EnterState()
    {
        base.EnterState();
        Context.inputContext.RestartDownEvent.AddListener(CombatRestartLevel);
        Context.playerModelTransform.gameObject.SetActive(false);
        Context.DeathParticles.Play();
        Context.colliderSwitcher.GetConcreteCollider().enabled = false;

        // Scarf stuff
        Context.scarfCloth.externalAcceleration = new Vector3(0, 100, 0);
        scarfRandAccel = Context.scarfCloth.randomAcceleration;
        Context.scarfCloth.randomAcceleration = new Vector3(15, 15, 15);
        scarfColliders = Context.scarfCloth.capsuleColliders;
        Context.scarfCloth.capsuleColliders = null;

        // Audio
        Context.audioManager.deathEmitter.Play();
    }

    public override void ExitState()
    {
        base.ExitState();
        Context.inputContext.RestartDownEvent.RemoveListener(CombatRestartLevel);
        Context.playerModelTransform.gameObject.SetActive(true);
        
        // Scarf stuff
        Context.colliderSwitcher.GetConcreteCollider().enabled = true;
        Context.scarfCloth.externalAcceleration = new Vector3(0, 0, 0);
        Context.scarfCloth.randomAcceleration = scarfRandAccel;
        Context.scarfCloth.capsuleColliders = scarfColliders;
        Context.primaryAttackCooldownTimer = 0.01f;
    }

    public override void FixedUpdateState() 
    { 
    
    }

    public override void UpdateState()
    {
        //base.UpdateState();
    }

    public override void CheckSwitchState() 
    { 
    
    }

    protected override void PlayerCombatKilled()
    {
        // Do nothing
    }
    protected virtual void CombatRestartLevel()
    {
        Context.audioManager.respawnEmitter.Play();
        Context.RespawnParticles.Play();
        Context.inputContext.RestartDownEvent.RemoveListener(CombatRestartLevel);
        Context.StartCoroutine(CoroutRestart());
    }

    protected virtual IEnumerator CoroutRestart()
    {
        yield return new WaitForSeconds(0.55f);
        Context.playerEvents.OnRestartLevel?.Invoke();
        TrySwitchState(Factory.CombatIdle);
    }
}
