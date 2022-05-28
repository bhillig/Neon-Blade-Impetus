using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeVFXManager : MonoBehaviour
{
    public PlayerEventsAsset playerEvents;
    public GameObject sword;
    public MeshRenderer scabbard;
    public GameObject scabbardSword;
    public ParticleSystem dryDashParticles;
    public ParticleSystem targettedDashParticles;
    public ParticleSystem dashTrailParticles;
    public ParticleSystem chargeParticles;
    public ParticleSystem chargeReadyParticles;
    public ParticleSystem cooldownFinishedParticles;
    public SkinnedMeshRenderer coat;

    [Header("Values")]
    public float endDelay;
    public Material scabbardDefaultGlow;
    public Material chargeStrikeMat;
    public Material defaultCoatMat;

    public Material scabbardCooldownMat;

    private void Awake()
    {
        playerEvents.OnStrikeStart += StrikePerformed;
        playerEvents.OnStrikeEnd += StrikeEnd;
        playerEvents.OnStrikeChargeReady += ChargeReady;
        playerEvents.OnStrikeChargeEnd += ChargeEnd;
        playerEvents.OnStrikeChargeStart += ChargeStart;
        playerEvents.OnStrikeCooldownFinished += CooldownFinished;
        playerEvents.OnStrikeCooldownStarted += CooldownStarted;
        sword.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnDestroy()
    {
        playerEvents.OnStrikeStart -= StrikePerformed;
        playerEvents.OnStrikeEnd -= StrikeEnd;
        playerEvents.OnStrikeChargeReady -= ChargeReady;
        playerEvents.OnStrikeChargeEnd -= ChargeEnd;
        playerEvents.OnStrikeChargeStart -= ChargeStart;
        playerEvents.OnStrikeCooldownFinished -= CooldownFinished;
        playerEvents.OnStrikeCooldownStarted -= CooldownStarted;
    }

    private void CooldownFinished()
    {
        cooldownFinishedParticles.Play();
        scabbard.material = scabbardDefaultGlow;
    }

    private void CooldownStarted()
    {
        scabbard.material = scabbardCooldownMat;
    }

    private void StrikePerformed(Collider target)
    {
        if(target == null)
        {
            DryDash();
        }
        else
        {
            TargetedDash();
        }
    }

    private void TargetedDash()
    {
        sword.GetComponent<MeshRenderer>().enabled = true;
        dashTrailParticles.Play();
        targettedDashParticles.Play();
    }

    private void ChargeReady()
    {
        DashReadyVisuals();
    }

    private void ChargeStart()
    {
        chargeParticles.Play();
    }

    private void ChargeEnd(bool dash)
    {
        chargeParticles.Stop();
        if (!dash)
            DefaultVisuals();
    }

    private void DashReadyVisuals()
    {
        scabbard.material = chargeStrikeMat;
        var mats = coat.materials;
        mats[0] = chargeStrikeMat;
        coat.materials = mats;
        chargeReadyParticles.Play();
    }

    private void DefaultVisuals()
    {
        scabbard.material = scabbardDefaultGlow;
        var mats = coat.materials;
        mats[0] = defaultCoatMat;
        coat.materials = mats;
    }

    private void StrikeEnd()
    {
        DefaultVisuals();
        StartCoroutine(CoroutStrikeEnd());
    }

    private IEnumerator CoroutStrikeEnd()
    {
        dashTrailParticles.Stop();
        yield return new WaitForSeconds(endDelay);
        scabbardSword.SetActive(true);
        sword.GetComponent<MeshRenderer>().enabled = false;
    }

    private void DryDash()
    {
        dryDashParticles.Play();
    }
}
