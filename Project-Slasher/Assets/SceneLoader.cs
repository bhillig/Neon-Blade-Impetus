using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string coreScene;

    [SerializeField]
    private string currentLevelScene;

    [SerializeField]
    private bool isLevel;

    void Awake()
    {
        // If this SceneLoader object is in a level scene, load the core utilities
        if(isLevel)
        {
            LoadCoreFunctionality();
        }
        // SceneManager.LoadScene("ParticleEffectScene", LoadSceneMode.Additive);
    }

    private void LoadCoreFunctionality()
    {
        SceneManager.LoadScene(coreScene, LoadSceneMode.Additive);
    }

    public void LoadNextLevel(string nextLevelScene)
    {
        SceneManager.LoadScene(nextLevelScene, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentLevelScene);
    }
}
