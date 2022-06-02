using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class PlayerAudioManager : MonoBehaviour
{
    [Header("Movement")]
    public StudioEventEmitter footStepEmitter;
    public StudioEventEmitter jumpEmitter;
    public StudioEventEmitter slideEmitter;
    public StudioEventEmitter rollEmitter;
    public StudioEventEmitter defaultLandEmitter;
    public StudioEventEmitter windSpeedEmitter;
    public StudioEventEmitter deathEmitter;
    public StudioEventEmitter respawnEmitter;

    [Header("Chargeup Strike")]
    public StudioEventEmitter chargeupWhineEmitter;
    public StudioEventEmitter chargeupReadyEmitter;
    public StudioEventEmitter chargeupOvercharge;
    public StudioEventEmitter targettrikeStart;
    public StudioEventEmitter targetStrikeImpact;
    public StudioEventEmitter dryStrikeDashImpact;


    public static void SetGlobalParameter(string name, float val)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(name, val);
    }

    public static float GetGobalParameter(string name)
    {
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName(name, out float res);
        return res;
    }
}
