using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornePhysicsContext : MonoBehaviour
{
    public LayerMask collisionMask;
    private int contactCount;
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.IsInLayerMask(collisionMask))
        {
            contactCount++;
        }
    }

    public void OnCollisionExitr(Collision collision)
    {
        if (collision.gameObject.IsInLayerMask(collisionMask))
        {
            contactCount--;
        }
    }

    public bool IsInContact()
    {
        return contactCount > 0;
    }
}
