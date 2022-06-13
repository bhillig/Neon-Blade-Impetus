using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimeTrialTrigger : MonoBehaviour
{
    [SerializeField]
    private TimeTrial timeTrial;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponentInParent<PlayerController>();
        if(pc != null && !triggered)
        {
            triggered = true;
            timeTrial.FinishTimeTrial();
        }
    }
}
