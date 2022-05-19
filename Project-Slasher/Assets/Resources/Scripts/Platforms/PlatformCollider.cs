using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        var platformSticker = collider.attachedRigidbody; 
        if (platformSticker)
        {
            collider.transform.parent = transform; 
        }
    }

    void OnTriggerExit(Collider collider) 
    {
        var platformSticker = collider.attachedRigidbody; 
        if (platformSticker)
        {
            collider.transform.parent = null; 
        }
    }
}
