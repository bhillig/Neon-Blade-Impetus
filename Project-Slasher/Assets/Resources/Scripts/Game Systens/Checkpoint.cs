using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool alreadyTriggered = false;
    private PlayerController pc;

    private void OnTriggerEnter(Collider other)
    {
        pc = other.transform.parent.GetComponent<PlayerController>();
        if(pc != null && !alreadyTriggered)
        {
            TriggerCheckpoint();
        }
    }

    private void TriggerCheckpoint()
    {
        Debug.Log("Checkpoint Triggered: " + gameObject.name);

        alreadyTriggered = true;
        pc.RespawnPoint = transform.position;
    }
}
