using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerEventsAsset playerEvents;

    private bool alreadyTriggered = false;
    private PlayerController pc;

    [SerializeField]
    private int checkPointID;
    public int CheckPointID {  get { return checkPointID; } }

    public ParticleSystem checkpointIndicator;
    public ParticleSystem checkpointTriggered;

    private void Awake()
    {
        playerEvents.OnRestartLevel += TeleportToCheckpoint;
    }

    private void OnDestroy()
    {
        playerEvents.OnRestartLevel -= TeleportToCheckpoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        var hit = other.GetComponentInParent<PlayerController>();
        if(hit != null && !alreadyTriggered)
        {
            pc = hit;
            TriggerCheckpoint();
        }
    }

    private void TriggerCheckpoint()
    {
        alreadyTriggered = true;
        pc.RespawnPoint = transform.position;
        RespawnHandler rh = FindObjectOfType<RespawnHandler>();
        checkpointTriggered?.Play();
        checkpointIndicator?.Stop();
        if(rh != null)
        {
            rh.SetRespawnID(checkPointID);
        }
    }

    private void TeleportToCheckpoint()
    {
        if (pc != null)
        {
            pc.playerPhysicsTransform.position = pc.RespawnPoint;
        }
    }
}
