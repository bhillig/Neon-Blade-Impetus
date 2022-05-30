using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyEntityCore : MonoBehaviour
{
    [Header("Components")]
    public List<Collider> colliders;
    public List<Renderer> renderers;
    public List<ParticleSystem> deathParticles;

    // Events
    public Action OnDead;
    public Action OnRespawn;

    private bool isDead;
    public bool IsDead => isDead;

    public void KillEnemy()
    {
        isDead = true;
        colliders.ForEach(collider => collider.enabled = false);
        renderers.ForEach(collider => collider.enabled = false);
        deathParticles.ForEach(deathParticle => deathParticle.Play());
        OnDead?.Invoke();
    }

    public void Respawn()
    {
        isDead = false; 
        colliders.ForEach(collider => collider.enabled = true);
        renderers.ForEach(collider => collider.enabled = true);
        OnRespawn?.Invoke();
    }

    [ContextMenu("Autofill fields")]
    public void AutoFillComponents()
    {
        colliders = new List<Collider>(gameObject.GetComponentsInChildren<Collider>());
        renderers = new List<Renderer>(gameObject.GetComponentsInChildren<Renderer>());
    }
}
