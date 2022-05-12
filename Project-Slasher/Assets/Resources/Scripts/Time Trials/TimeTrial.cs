using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrial : MonoBehaviour
{
    // state variables
    private float currentTime = 0.0f;
    private float finalTime = 0.0f;
    private bool isTimeTrialActive = false;

    public float CurrentTime
    {
        get { return currentTime; }
    }

    public float FinalTime
    {
        get { return finalTime; }
    }

    public bool IsTimeTrialActive
    {
        get { return isTimeTrialActive; }
    }

    void Update()
    {
        PassTime();
    }

    private void PassTime()
    {
        if (!isTimeTrialActive)
        {
            return;
        }

        currentTime += Time.deltaTime;
    }

    public void StartTimeTrial()
    {
        currentTime = 0.0f;
        isTimeTrialActive = true;
        FindObjectOfType<TimeTrialUI>().StartTimeTrial();
    }

    public void StopTimeTrial()
    {
        finalTime = currentTime;
        currentTime = 0.0f;
        isTimeTrialActive = false;
        FindObjectOfType<TimeTrialUI>().FinishTimeTrial();
    }

    public void ResetTimeTrial()
    {
        currentTime = 0.0f;
        isTimeTrialActive = false;
    }

    public void AddSeconds(float seconds)
    {
        currentTime += seconds;
    }

    public void SubSeconds(float seconds)
    {
        currentTime -= seconds;
    }

}
