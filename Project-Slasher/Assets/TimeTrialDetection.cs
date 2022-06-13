using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrialDetection : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> levelObjectsToUnload;

    [SerializeField]
    private List<GameObject> timeTrialObjectsToLoad;

    private void Awake()
    {
        SceneLoader sl = FindObjectOfType<SceneLoader>();
        if(sl != null)
        {
            if(sl.Scene_Mode == SceneLoader.SceneMode.TimeTrialLevel)
            {
                LoadTimeTrial();
            }
            else
            {
                // do nothing (game plays as is a regular level)
            }
        }
    }

    private void LoadTimeTrial()
    {
        UnloadLevelObjects();
        LoadTimeTrialObjects();
    }

    private void UnloadLevelObjects()
    {
        foreach (GameObject obj in levelObjectsToUnload)
        {
            obj.SetActive(false);
        }
    }
    private void LoadTimeTrialObjects()
    {
        foreach (GameObject obj in timeTrialObjectsToLoad)
        {
            obj.SetActive(true);
        }
    }
}
