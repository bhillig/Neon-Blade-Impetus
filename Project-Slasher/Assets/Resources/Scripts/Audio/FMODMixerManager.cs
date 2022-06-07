using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMixerManager : MonoBehaviour
{
    public static FMODMixerManager instance;

    FMOD.Studio.Bus master;// = FMODUnity.RuntimeManager.GetBus("bus:/");
    FMOD.Studio.Bus sfx;// = FMODUnity.RuntimeManager.GetBus("bus:/");
    FMOD.Studio.Bus music;// = FMODUnity.RuntimeManager.GetBus("bus:/");

    private void Awake()
    {
        instance = this;
        master = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        sfx = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
    }

    public void SetSFXVolume(float val)
    {
        sfx.setVolume(val);
    }

    public void SetMusicVolume(float val)
    {
        music.setVolume(val);
    }

    public void SetMasterVolume(float val)
    {
        master.setVolume(val);
    }
}
