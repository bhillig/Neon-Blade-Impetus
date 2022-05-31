using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVoid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var pc = other.GetComponentInParent<PlayerHitboxManager>();
        if (pc != null)
        {
            pc.playerEvents.OnCollideWithVoid();
        }
    }
}
