using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void StartSession(string sceneName)
    {
        sceneLoader.StartSession(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        sceneLoader.LoadNextLevel(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
