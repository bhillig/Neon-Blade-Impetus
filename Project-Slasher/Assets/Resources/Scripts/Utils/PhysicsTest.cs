using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTest : MonoBehaviour
{
    private Vector3 pos;
    public Vector3 vel;

    private void Awake()
    {
        pos = transform.position;
    }
    [ContextMenu("Shove")]
    public void Shove()
    {
        GetComponent<Rigidbody>().velocity = vel;
    }

    [ContextMenu("Test Reset")]
    public void Reset()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = pos;
    }
}
