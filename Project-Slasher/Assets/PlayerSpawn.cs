using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        MovePlayerToSpawn();
    }

    private void MovePlayerToSpawn()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = transform.position;
            player.GetComponentInParent<PlayerController>().TPTargetController.AlignCameraRotation(transform);
            player.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            Debug.Log("PlayerSpawn can't find Player!");
        }
    }

}
