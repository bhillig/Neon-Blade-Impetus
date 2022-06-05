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
    private string mainMenuScene;

    [SerializeField]
    private string debug_SceneYouWantToPlay;

    [SerializeField]
    private bool loadOnStart;

    private void Start()
    {
        if (loadOnStart)
            StartSession(debug_SceneYouWantToPlay);
        else
            currentLevelScene = debug_SceneYouWantToPlay;
    }

    public void StartSession(string level)
    {
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
        currentLevelScene = level;
    }

    public void EndSession()
    {
        SceneManager.UnloadSceneAsync(currentLevelScene);
        SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(coreScene);
    }
    public void LoadNextLevel(string nextLevelScene)
    {
        SceneManager.UnloadSceneAsync(currentLevelScene);
        SceneManager.LoadScene(nextLevelScene, LoadSceneMode.Additive);
        currentLevelScene = nextLevelScene;
    }
}
