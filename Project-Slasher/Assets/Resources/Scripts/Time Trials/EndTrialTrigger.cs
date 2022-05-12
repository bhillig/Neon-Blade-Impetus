using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrialTrigger : MonoBehaviour
{
    private bool timeTrialStopped = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.CompareTag("Player") && !timeTrialStopped)
        {
            FindObjectOfType<TimeTrial>().StopTimeTrial();
            timeTrialStopped = true;
        }
    }
}
