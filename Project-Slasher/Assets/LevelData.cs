using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [System.Serializable]
    public struct Level
    {
        public string name;
        public bool completed;
    }

    [SerializeField]
    private int numberOfLevels;

    [SerializeField]
    private List<Level> levels;

    public void SetLevelComplete(int index)
    {
        if (index < 0 || index > numberOfLevels) return;

        Level v = levels[index];
        v.completed = true;
        levels[index] = v;
    }

    public bool GetLevelComplete(int index)
    {
        if (index < 0 || index > numberOfLevels) return false;
        return levels[index].completed;
    }

    public string GetSceneString(int index)
    {
        return levels[index].name;
    }
}
