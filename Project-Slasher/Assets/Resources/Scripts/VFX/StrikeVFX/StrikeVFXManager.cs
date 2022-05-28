using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeVFXManager : MonoBehaviour
{
    public PlayerEventsAsset playerEvents;
    public GameObject sword;
    public GameObject scabbard;
    public GameObject scabbardSword;
    public ParticleSystem dryDashParticles;
    public ParticleSystem targettedDashParticles;
    public ParticleSystem dashTrailParticles;
    public SkinnedMeshRenderer coat;

    [Header("Values")]
    public float endDelay;
    public Material defaultMat;
    public Material chargeStrikeMat;
    public Material defaultCoatMat;

    private void Awake()
    {
        playerEvents.OnStrikeStart += StrikePerformed;
        playerEvents.OnStrikeEnd += StrikeEnd;
        playerEvents.OnStrikeChargeReady += ChargeReady;
        playerEvents.OnStrikeChargeEnd += ChargeEnd;
        sword.GetComponent<MeshRenderer>().enabled = false;
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

    private void ChargeEnd(bool dash)
    {
        if(!dash)
            DefaultVisuals();
    }

    private void DashReadyVisuals()
    {
        scabbard.GetComponent<MeshRenderer>().material = chargeStrikeMat;
        var mats = coat.materials;
        mats[0] = chargeStrikeMat;
        coat.materials = mats;
    }

    private void DefaultVisuals()
    {
        scabbard.GetComponent<MeshRenderer>().material = defaultMat;
        var mats = coat.materials;
        mats[0] = defaultCoatMat;
        coat.materials = mats;
    }

    private void StrikeEnd()
    {
        StartCoroutine(CoroutStrikeEnd());
    }

    private IEnumerator CoroutStrikeEnd()
    {
        dashTrailParticles.Stop();
        yield return new WaitForSeconds(endDelay);
        DefaultVisuals();
        scabbardSword.SetActive(true);
        sword.GetComponent<MeshRenderer>().enabled = false;
    }

    private void DryDash()
    {
        dryDashParticles.Play();
    }

    private void OnDestroy()
    {
        playerEvents.OnStrikeStart -= StrikePerformed;
        playerEvents.OnStrikeEnd -= StrikeEnd;
    }
}
