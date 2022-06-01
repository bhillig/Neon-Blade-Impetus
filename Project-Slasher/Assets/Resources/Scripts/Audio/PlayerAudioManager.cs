using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class PlayerAudioManager : MonoBehaviour
{
    [Header("FMOD Studio Events")]
    public StudioEventEmitter footStepEmitter;
    public StudioEventEmitter jumpEmitter;
    public StudioEventEmitter slideEmitter;
    public StudioEventEmitter rollEmitter;
    public StudioEventEmitter defaultLandEmitter;
    public StudioEventEmitter windSpeedEmitter;
    public StudioEventEmitter deathEmitter;
    public StudioEventEmitter respawnEmitter;


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
