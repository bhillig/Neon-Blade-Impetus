using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderEvents : MonoBehaviour
{
    public Action<Collision> OnCollisionEnterEvent;

    public void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent?.Invoke(collision);
    }
}
