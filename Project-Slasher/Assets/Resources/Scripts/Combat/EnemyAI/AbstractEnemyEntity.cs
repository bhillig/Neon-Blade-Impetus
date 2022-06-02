using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractEnemyEntity : MonoBehaviour
{
    // Events
    public Action OnDead;
    public Action OnRespawn;

    protected bool isDead;
    public bool IsDead => isDead;

    public abstract void KillEnemy();
    public abstract void Respawn();
}
