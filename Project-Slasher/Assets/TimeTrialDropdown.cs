using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TimeTrialDropdown : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> levelCanvases;

    public void DisplayLevelCanvas(int levelNumberIndex)
    {
        for(int i = 0; i < levelCanvases.Count; i++)
        {
            if(i == levelNumberIndex)
            {
                levelCanvases[i].SetActive(true);
                continue;
            }
            levelCanvases[i].SetActive(false);
        }
    }
}
