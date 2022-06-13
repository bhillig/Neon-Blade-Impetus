using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private PlayerEventsAsset _playerEventsAsset;

    [SerializeField]
    private TextMeshProUGUI countdownText;

    private float currentTimeDuration = 3.0f;

    private float timeDuration = 0.0f;

    private bool finished = false;

    private void Awake()
    {
        _playerEventsAsset.OnRestartLevel += ResetCountdown;
    }

    private void OnDestroy()
    {
        _playerEventsAsset.OnRestartLevel -= ResetCountdown;
    }

    private void Update()
    {
        countdownText.text = currentTimeDuration.ToString("F0");

        if(currentTimeDuration < timeDuration)
        {
            currentTimeDuration = 0.0f;
            FinishCountdown();
        }

        currentTimeDuration -= Time.deltaTime;

    }

    private void FinishCountdown()
    {
        finished = true;
    }

    public void ResetCountdown()
    {
        currentTimeDuration = 3.0f;
        finished = false;
    }
    
}
