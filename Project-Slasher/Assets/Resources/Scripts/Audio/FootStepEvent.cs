using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepEvent : MonoBehaviour
{
    public PlayerEventsAsset playerEvent;

    public void TriggerFootstepEvent()
    {
        playerEvent.FootstepTaken?.Invoke();
    }
}
