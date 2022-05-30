using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVoid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.transform.parent.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.Die();
        }
    }
}
