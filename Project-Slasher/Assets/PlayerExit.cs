using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    [SerializeField]
    private string nextLevelScene;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.transform.parent.gameObject.GetComponent<PlayerController>();
        if(pc != null)
        {
            FindObjectOfType<SceneLoader>().LoadNextLevel(nextLevelScene);
        }
    }
}
