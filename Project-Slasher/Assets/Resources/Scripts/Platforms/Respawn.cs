using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject player; 
    [SerializeField] float fallThreshhold = -10f; 
    [SerializeField] GameObject spawn; 
    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < fallThreshhold) {
            player.transform.position = spawn.transform.position; 
        }
    }
}
