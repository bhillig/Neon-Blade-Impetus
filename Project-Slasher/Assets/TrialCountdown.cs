using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialCountdown : MonoBehaviour
{
    [SerializeField]
    PlayerEventsAsset _playerEventsAsset;

    [SerializeField]
    private GameObject startWall;

    [SerializeField]
    private GameObject countdownCanvas;

    [SerializeField]
    private float countdownDuration;

    private float currentCountdownDuration = 0.0f;

    private bool isCountdownTicking = false;

    private bool startedTimeTrial = false;

    private void Awake()
    {
        _playerEventsAsset.OnRestartLevel += ResetTimeTrialCountdown;
    }

    private void OnDestroy()
    {
        _playerEventsAsset.OnRestartLevel -= ResetTimeTrialCountdown;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponentInParent<PlayerController>();
        if(playerController != null && !startedTimeTrial)
        {
            StartCountdown();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponentInParent<PlayerController>();
        if (playerController != null && !startedTimeTrial)
        {
            StopCountdown();
        }
    }

    private void Update()
    {
        if(isCountdownTicking)
        {
            Debug.Log(currentCountdownDuration);
            if(currentCountdownDuration >= countdownDuration)
            {
                currentCountdownDuration = 0.0f;
                StopCountdown();
                StartTimeTrial();
            }
            currentCountdownDuration += Time.deltaTime;
        }
    }

    private void StartCountdown()
    {
        isCountdownTicking = true;
        countdownCanvas.SetActive(true);
    }

    private void StopCountdown()
    {
        isCountdownTicking = false;
        currentCountdownDuration = 0.0f;
        countdownCanvas.GetComponent<CountdownTimer>().ResetCountdown();
        countdownCanvas.SetActive(false);
    }

    private void StartTimeTrial()
    {
        startedTimeTrial = true;
        startWall.gameObject.SetActive(false);

        TimeTrial timeTrial = FindObjectOfType<TimeTrial>();
        if(timeTrial != null)
        {
            timeTrial.StartTimeTrial();
        }
    }

    private void ResetTimeTrialCountdown()
    {
        startWall.gameObject.SetActive(true);
        isCountdownTicking = false;
        startedTimeTrial = false;
    }
}
