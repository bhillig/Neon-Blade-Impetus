using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreUtilities : MonoBehaviour
{
    public static CoreUtilities Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
