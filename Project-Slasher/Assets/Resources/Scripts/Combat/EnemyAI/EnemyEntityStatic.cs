using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyEntityStatic : AbstractEnemyEntity
{
    
    public override void KillEnemy()
    {
        OnDead?.Invoke();
    }

    public override void Respawn()
    {
        OnRespawn?.Invoke();
    }
}
