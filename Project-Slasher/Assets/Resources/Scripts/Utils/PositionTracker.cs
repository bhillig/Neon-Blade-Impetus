using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    public bool releaseFromParent;

    private void Start()
    {
        offset = target.position - transform.position;
        if (releaseFromParent)
            transform.parent = null;
    }

    private void Update()
    {
        if(target != null)
            transform.position = target.position - offset;
    }
}
