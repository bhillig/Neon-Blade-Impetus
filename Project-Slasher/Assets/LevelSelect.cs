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
            if (!CanLoadLevel(i))
            {
                lockIcons[i].SetActive(true);
            }
            else
            {
                lockIcons[i].SetActive(false);
                if (lockIcons[i+1] != null)
                {
                    lockIcons[i+1].SetActive(false);
                }
            }
        }
    }

    public void LoadLevel(int levelNumber)
    {
        // If you completed the previous level...
        if (CanLoadLevel(levelNumber))
        {
            string levelToLoad = levelData.GetSceneString(levelNumber);
            sceneLoader.StartSession(levelToLoad);
        }
    }

    private bool CanLoadLevel(int levelNumber)
    {
        if (levelNumber < 0 || levelNumber >= lockIcons.Count) return false;
        return levelData.GetLevelComplete(levelNumber - 1);
    }
}