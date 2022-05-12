using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTrialUI : MonoBehaviour
{
    // config parameters
    [SerializeField]
    TextMeshProUGUI currentTimeText;

    [SerializeField]
    TextMeshProUGUI finalTimeTextForOverlay;

    [SerializeField]
    TextMeshProUGUI bestTimeTextForOverlay;

    [SerializeField]
    GameObject timer;

    [SerializeField]
    GameObject timeTrialOverCanvas;

    // cache references
    private TimeTrial timeTrial;

    private void Start()
    {
        timeTrial = FindObjectOfType<TimeTrial>();
        timer.SetActive(false);
    }

    void Update()
    {
        if(timeTrial.IsTimeTrialActive)
        {
            currentTimeText.text = timeTrial.CurrentTime.ToString("F2");
            
        }
        else
        {
            currentTimeText.text = timeTrial.FinalTime.ToString("F2");
            finalTimeTextForOverlay.text = timeTrial.FinalTime.ToString("F2");

            // this line of code will change when the best time system gets implemented
            bestTimeTextForOverlay.text = timeTrial.FinalTime.ToString("F2");
        }
    }

    public void StartTimeTrial()
    {
        timer.SetActive(true);
    }

    public void FinishTimeTrial()
    {
        EnableOverlay();
        DisableTimer();
    }

    private void EnableOverlay()
    {
        if(!timeTrialOverCanvas.activeSelf)
        {
            timeTrialOverCanvas.SetActive(true);
        }
    }

    private void DisableTimer()
    {
        timer.SetActive(false);
    }
}
