using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var pc = other.GetComponentInParent<PlayerHitboxManager>();
        if (pc != null)
        {
            pc.playerEvents.OnCollideWithVoid();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        var pc = other.collider.GetComponentInParent<PlayerHitboxManager>();
        if (pc != null)
        {
            pc.playerEvents.OnCollideWithVoid();
        }
    }
}
