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
    }

    public void RespawnEnemies()
    {
        Debug.Log("Called");
       foreach (var enemy in enemySections[currentCheckpointID].enemies)
        {
            enemy.Respawn();
        }
    }

    public void SetRespawnID(int checkpointID)
    {
        currentCheckpointID = checkpointID;
    }
}
