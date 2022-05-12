using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrialTrigger : MonoBehaviour
{
    private bool timeTrialStarted = false;

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.parent.CompareTag("Player") && !timeTrialStarted)
        {
            FindObjectOfType<TimeTrial>().StartTimeTrial();
            timeTrialStarted = true;
        }
    }
}
