using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrialData : MonoBehaviour
{
    [System.Serializable]
    class BestTimes
    {
        public string levelPrefCode;
        public float bestTime; // local best time on the program
    }

    [SerializeField]
    List<BestTimes> bestTimes;

    void Start()
    {
        SyncBestTimes();
    }

    // sets the local best times to that of the player pref
    void SyncBestTimes()
    {
        foreach(var t in bestTimes)
        {
            t.bestTime = PlayerPrefs.GetFloat(t.levelPrefCode);

            // if the best time hasnt been set yet, we want the maximum float value since the player can never beat zero
            if(t.bestTime <= 0.0f)
            {
                t.bestTime = float.MaxValue;
            }
        }
    }

    public float GetBestTime(int levelNumber)
    {
        return bestTimes[levelNumber].bestTime;
    }

    public void SetBestTime(int levelNumber, float time)
    {
        bestTimes[levelNumber].bestTime = time; // local set
        PlayerPrefs.SetFloat(bestTimes[levelNumber].levelPrefCode, time); // lifetime set
    }


}
