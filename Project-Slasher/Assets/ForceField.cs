using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField]
    private List<AbstractEnemyEntity> enemiesNeededToKill;

    private bool isOpen = false;
    public bool IsOpen { get { return isOpen; } set { isOpen = value; } } 


    public void CheckOpenCondition()
    {
        foreach(var enemy in enemiesNeededToKill)
        {
            if(!enemy.IsDead)
            {
                return;
            }
        }
        OpenForceField();
    }

    public void OpenForceField()
    {
        isOpen = true;
        this.gameObject.SetActive(false);
    }

    public void Respawn()
    {
        isOpen = false;
        this.gameObject.SetActive(true);
    }
}
