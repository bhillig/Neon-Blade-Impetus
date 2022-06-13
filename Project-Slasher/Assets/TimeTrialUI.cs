using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrialUI : MonoBehaviour
{
    [SerializeField]
    private GameObject timerCanvas;

    [SerializeField]
    private GameObject resultsCanvas; 

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI timerResultsText;

    [SerializeField]
    private TextMeshProUGUI bestTimeResultText;

    [SerializeField]
    private TimeTrial timeTrial;

    private void Update()
    {
        timerText.text = timeTrial.CurrentTimer.ToString("F2");
        timerResultsText.text = timeTrial.CurrentTimer.ToString("F2");
        bestTimeResultText.text = timeTrial.CurrentTimer.ToString("F2");
    }

    public void ShowResults()
    {
        resultsCanvas.SetActive(true);
        Cursor.visible = true;
    }

    public void ShowTimer()
    {
        timerCanvas.SetActive(true);
    }

    public void HideTimer()
    {
        timerCanvas.SetActive(false);
    }
}
