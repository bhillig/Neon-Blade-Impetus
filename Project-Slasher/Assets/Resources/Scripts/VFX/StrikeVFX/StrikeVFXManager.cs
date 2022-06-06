using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeVFXManager : MonoBehaviour
{
    [Header("Dependencies")]
    public PlayerEventsAsset playerEvents;

    [Header("Chargeup Particles")]
    public ParticleSystem chargeParticles;
    public ParticleSystem chargeReadyParticles;
    public ParticleSystem cooldownFinishedParticles;
    public ParticleSystem overchargedParticles;

    [Header("Targeted Strike Particles")]
    public ParticleSystem targetedStrikeSonicBoomParticles;
    public ParticleSystem targetedStrikeSonicBoom2Particles;
    public TrailRenderer targetedStrikeTrail;

    [Header("Dry Strike Particles")]
    public ParticleSystem dryStrikeTrailParticles;
    public ParticleSystem dryStrikeSonicBoomParticles;

    [Header("Renderers")]
    public MeshRenderer sword;
    public MeshRenderer scabbard;
    public MeshRenderer scabbardSword;
    public SkinnedMeshRenderer coat;
    public SkinnedMeshRenderer body;
    public SkinnedMeshRenderer scarfRenderer;

    [Header("Values")]
    public float endDelay;

    [Header("Materials")]
    public Material scabbardDefaultGlow;
    public Material chargeStrikeMat;
    public Material defaultCoatMat;
    public Material scabbardCooldownMat;

    [Header("Scarf")]
    [ColorUsage(true, true)]
    public Color scarfDefaultGlow;
    [ColorUsage(true, true)]
    public Color scarfChargedGlow;
    [ColorUsage(true, true)]
    public Color scarfCooldownGlow;

    private void Awake()
    {
        playerEvents.OnStrikeStart += StrikePerformed;
        playerEvents.OnStrikeEnd += StrikeEnd;
        playerEvents.OnStrikeChargeReady += ChargeReady;
        playerEvents.OnStrikeChargeEnd += ChargeEnd;
        playerEvents.OnStrikeChargeStart += ChargeStart;
        playerEvents.OnStrikeOvercharged += Overcharged;
        playerEvents.OnStrikeCooldownFinished += CooldownFinished;
        playerEvents.OnStrikeCooldownStarted += CooldownStarted;
        playerEvents.ImpactEnd += ImpactEnd;
        sword.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnDestroy()
    {
        playerEvents.OnStrikeStart -= StrikePerformed;
        playerEvents.OnStrikeEnd -= StrikeEnd;
        playerEvents.OnStrikeChargeReady -= ChargeReady;
        playerEvents.OnStrikeChargeEnd -= ChargeEnd;
        playerEvents.OnStrikeChargeStart -= ChargeStart;
        playerEvents.OnStrikeOvercharged -= Overcharged;
        playerEvents.OnStrikeCooldownFinished -= CooldownFinished;
        playerEvents.OnStrikeCooldownStarted -= CooldownStarted;
        playerEvents.ImpactEnd -= ImpactEnd;
    }

    private void CooldownFinished()
    {
        cooldownFinishedParticles.Play();
        DefaultVisuals();
    }

    private void CooldownStarted()
    {
        scabbard.material = scabbardCooldownMat;
        coat.SetMaterials(new int[] { 0 }, new Material[] { scabbardCooldownMat });
        body.SetMaterials(new int[] { 9, 10 }, new Material[] { scabbardCooldownMat });
        SetScarfEmission(scarfCooldownGlow);
    }

    private void StrikePerformed(AbstractEnemyEntity target)
    {
        chargeParticles.Stop();
        chargeReadyParticles.Stop();
        if (target == null)
        {
            DryStrike();
        }
        else
        {
            TargetedStrike();
        }
    }

    private void TargetedStrike()
    {
        scabbardSword.enabled = false;
        sword.enabled = true;
        StartCoroutine(CoroutFrameDelay(() => targetedStrikeTrail.emitting = true));
        targetedStrikeSonicBoomParticles.Play();
    }

    private IEnumerator CoroutFrameDelay(System.Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }

    private void ChargeReady()
    {
        DashReadyVisuals();
    }

    private void ChargeStart()
    {
        chargeParticles.Play();
    }

    private void Overcharged()
    {
        overchargedParticles.Play();
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
        coat.SetMaterials(new int[] { 0 }, new Material[] {chargeStrikeMat});
        body.SetMaterials(new int[] { 9, 10 }, new Material[] { chargeStrikeMat });
        chargeReadyParticles.Play();
        chargeParticles.Stop();
        SetScarfEmission(scarfChargedGlow);
    }

    private void DefaultVisuals()
    {
        chargeParticles.Stop();
        chargeReadyParticles.Stop();
        scabbard.material = scabbardDefaultGlow;
        coat.SetMaterials(new int[] { 0 }, new Material[] { defaultCoatMat });
        body.SetMaterials(new int[] { 9, 10 }, new Material[] { defaultCoatMat });
        SetScarfEmission(scarfDefaultGlow);
    }

    private void SetScarfEmission(Color c)
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        scarfRenderer.GetPropertyBlock(block);
        block.SetColor("_Emission", c);
        scarfRenderer.SetPropertyBlock(block);
    }

    private void StrikeEnd()
    {
        DefaultVisuals();
        StartCoroutine(CoroutStrikeEnd());
        targetedStrikeTrail.emitting = false;
        targetedStrikeTrail.Clear();
    }

    private void ImpactEnd(float _)
    {
        targetedStrikeSonicBoomParticles?.Stop();
        targetedStrikeSonicBoom2Particles?.Play();
    }

    private IEnumerator CoroutStrikeEnd()
    {
        yield return new WaitForSeconds(endDelay);
        scabbardSword.enabled = true;
        sword.enabled = false;
    }

    private void DryStrike()
    {
        dryStrikeSonicBoomParticles.Play();
        dryStrikeTrailParticles.Play();
    }
}
