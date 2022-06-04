using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    [SerializeField]
    private string nextLevelScene;

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.transform.parent.gameObject.GetComponent<PlayerController>();
        if(pc != null && !triggered)
        {
            triggered = true;
            FindObjectOfType<SceneLoader>().LoadNextLevel(nextLevelScene);
        }
    }
}
