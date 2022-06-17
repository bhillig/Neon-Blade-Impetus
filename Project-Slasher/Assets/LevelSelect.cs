using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> lockIcons;

    private LevelData levelData;
    private SceneLoader sceneLoader;

    private void Awake()
    {
        levelData = FindObjectOfType<LevelData>();
        Debug.Log(levelData.HighestLevelCompleted);
        sceneLoader = FindObjectOfType<SceneLoader>();

        if (sceneLoader == null)
        {
            Debug.LogError("Error, no scene loader found!");
            return;
        }
        if (levelData == null)
        {
            Debug.LogError("Error, no level data found!");
            return;
        }
        
        for (int i = 1; i < lockIcons.Count; i++)
        {
            if(i <= levelData.HighestLevelCompleted + 1)
            {
                lockIcons[i].SetActive(false);
            }
            else
            {
                lockIcons[i].SetActive(true);
            }
        }
    }

    public void LoadLevel(int levelNumber)
    {
        // If you completed the previous level...
        if (CanLoadLevel(levelNumber))
        {
            Debug.Log("true");
            string levelToLoad = levelData.GetSceneString(levelNumber);
            sceneLoader.StartSession(levelToLoad);
            sceneLoader.Scene_Mode = SceneLoader.SceneMode.Level;
        }
    }

    public void LoadTimeTrialLevel(int levelNumber)
    {
        // If you completed the previous level...
        if (CanLoadLevel(levelNumber))
        {
            string levelToLoad = levelData.GetSceneString(levelNumber);
            sceneLoader.StartSession(levelToLoad);
            sceneLoader.Scene_Mode = SceneLoader.SceneMode.TimeTrialLevel;
        }
    }

    private bool CanLoadLevel(int levelNumber)
    {
        if (levelNumber < 0 || levelNumber >= lockIcons.Count) return false;
        return levelNumber <= levelData.HighestLevelCompleted + 1;
    }
}