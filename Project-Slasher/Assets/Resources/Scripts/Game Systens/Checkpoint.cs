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

    [Header("VFX/SFX")]
    public ParticleSystem checkpointIndicator;
    public ParticleSystem checkpointTriggered;
    public FMODUnity.StudioEventEmitter triggeredSound;

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
        triggeredSound?.Play();
        if(rh != null)
        {
            rh.SetRespawnID(checkPointID);
        }
    }

    private void TeleportToCheckpoint()
    {
        if (pc != null)
        {
            pc.TPTargetController.AlignCameraRotation(transform);
            pc.playerPhysicsTransform.position = pc.RespawnPoint;
        }
    }
}
