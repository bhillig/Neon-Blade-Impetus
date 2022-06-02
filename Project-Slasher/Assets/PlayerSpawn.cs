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
        player.transform.position = transform.position;
    }

}
