using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("ParticleEffectScene", LoadSceneMode.Additive);
    }
}
