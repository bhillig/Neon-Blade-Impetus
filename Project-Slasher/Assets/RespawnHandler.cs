using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnHandler : MonoBehaviour
{
    public PlayerEventsAsset playerEvents;

    [SerializeField]
    private int numberOfSections;

    [SerializeField]
    private List<ForceField> forceFields;

    [SerializeField]
    private List<EnemySection> enemySections;
    
    [System.Serializable]
    public struct EnemySection
    {
        public List<AbstractEnemyEntity> enemies;    
    }

    private int currentCheckpointID;


    private void Awake()
    {
        Debug.Log("test");
        playerEvents.OnRestartLevel += RespawnEnemies;
        playerEvents.OnRestartLevel += RespawnForceField;
    }

    private void OnDestroy()
    {
        playerEvents.OnRestartLevel -= RespawnEnemies;
        playerEvents.OnRestartLevel -= RespawnForceField;
    }

    public void RespawnEnemies()
    {
        if (currentCheckpointID >= enemySections.Count || enemySections[currentCheckpointID].enemies == null) return;
        foreach (var enemy in enemySections[currentCheckpointID].enemies)
        {
            enemy.Respawn();
        }
    }

    public void RespawnForceField()
    {
        if (currentCheckpointID >= forceFields.Count) return;
        if(forceFields[currentCheckpointID] != null && forceFields[currentCheckpointID].IsOpen)
        {
            forceFields[currentCheckpointID].RespawnForceField();
        }
    }

    public void SetRespawnID(int checkpointID)
    {
        currentCheckpointID = checkpointID;
    }
}
