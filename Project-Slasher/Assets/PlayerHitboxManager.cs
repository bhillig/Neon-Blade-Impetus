using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxManager : MonoBehaviour
{
    public PlayerEventsAsset playerEvents;

    public void HitByProjectile()
    {
        playerEvents.OnCollideWithProjectile?.Invoke();
    }
}
