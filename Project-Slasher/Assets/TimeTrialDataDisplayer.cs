using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialDataDisplayer : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> bestTimeScoresText;

    // cache references
    TimeTrialData timeTrialData;

    private void Awake()
    {
        timeTrialData = FindObjectOfType<TimeTrialData>();
    }

    private void Start()
    {
        int levelNumber = 0;
        for(int i = 0; i < bestTimeScoresText.Count; i++)
        {
            levelNumber = i + 1;
            float bestTime = timeTrialData.GetBestTime(levelNumber);
            if(bestTime == float.MaxValue)
            {
                bestTimeScoresText[i].text = "Not Completed";
                bestTimeScoresText[i].color = Color.red;
            }
            else
            {
                bestTimeScoresText[i].text = bestTime.ToString("F2") + " secs";
            }
            

        }
    }
}
