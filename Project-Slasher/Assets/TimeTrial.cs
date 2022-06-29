using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrial : MonoBehaviour
{
    [SerializeField]
    private PlayerEventsAsset _PlayerEventsAsset;

    [SerializeField]
    private TimeTrialUI timeTrialUI;

    private bool isTimeTrialActive = false;

    private float currentTimer = 0.0f;
    public float CurrentTimer { get { return currentTimer; } set { currentTimer = value; } }

    [SerializeField]
    private int levelNumber;

    // cache references
    TimeTrialData timeTrialData;
    LevelData levelData;

    private void Awake()
    {
        _PlayerEventsAsset.OnRestartLevel += StopTimeTrial;

        levelData = FindObjectOfType<LevelData>();
        timeTrialData = FindObjectOfType<TimeTrialData>();
    }

    private void OnDestroy()
    {
        _PlayerEventsAsset.OnRestartLevel -= StopTimeTrial;
    }

    private void Update()
    {
        if(isTimeTrialActive)
        {
            currentTimer += Time.deltaTime;
        }
    }

    public void StartTimeTrial()
    {
        currentTimer = 0.0f;
        isTimeTrialActive = true;
        timeTrialUI.ShowTimer();
    }

    public void StopTimeTrial()
    {
        isTimeTrialActive = false;
        timeTrialUI.HideTimer();
    }

    public void FinishTimeTrial()
    {
        StopTimeTrial();
        UpdateBestTime();
        timeTrialUI.ShowResults(levelNumber);
    }

    public void AddSeconds(float seconds)
    {
        currentTimer += seconds;
    }

    public void SubtractSeconds(float seconds)
    {
        currentTimer -= seconds;
        if(currentTimer < 0.0f)
        {
            currentTimer = 0.0f;
        }
    }

    private void UpdateBestTime()
    {
        if(timeTrialData != null)
        {
            if(currentTimer < timeTrialData.GetBestTime(levelNumber))
            {
                timeTrialData.SetBestTime(levelNumber, currentTimer);
            }
        }
    }
}
