using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCanvas : MonoBehaviour
{
    public RespawnCanvas Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
    }

    public void EnableRespawnCanvas()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableRespawnCanvas()
    {
        this.gameObject.SetActive(false);
    }
}
