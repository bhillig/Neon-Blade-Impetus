using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField]
    private List<AbstractEnemyEntity> enemiesNeededToKill;

    private bool isOpen = false;
    public bool IsOpen { get { return isOpen; } }   

    private void Awake()
    {
        foreach (var enemy in enemiesNeededToKill)
        {
            //enemy.OnRespawn += CheckOpenCondition;
            enemy.OnDead += CheckOpenCondition;
        }
    }

    private void OnDestroy()
    {
        foreach (var enemy in enemiesNeededToKill)
        {
            //enemy.OnRespawn -= CheckOpenCondition;
            enemy.OnDead -= CheckOpenCondition;
        }
    }


    public void CheckOpenCondition()
    {
        foreach (var enemy in enemiesNeededToKill)
        {
            if(!enemy.IsDead)
            {
                return;
            }
        }
        OpenForceField();
    }

    private void OpenForceField()
    {
        isOpen = true;
        StartCoroutine(CoroutOpenAnimation());
    }

    private IEnumerator CoroutOpenAnimation()
    {
        var materialPropBlock = new MaterialPropertyBlock();
        var ren = GetComponent<MeshRenderer>();
        ren.GetPropertyBlock(materialPropBlock);
        float timer = 0f;
        float time = 1f;
        while(timer < time)
        {
            timer += Time.deltaTime;
            float t = timer / time;
            materialPropBlock.SetFloat("_Strength", 1 - t);
            ren.SetPropertyBlock(materialPropBlock);
            yield return new WaitForEndOfFrame();
        }
        materialPropBlock.SetFloat("_Strength", 1);
        ren.SetPropertyBlock(materialPropBlock);
        this.gameObject.SetActive(false);
    }

    public void RespawnForceField()
    {
        isOpen = false;
        this.gameObject.SetActive(true);
    }
}
