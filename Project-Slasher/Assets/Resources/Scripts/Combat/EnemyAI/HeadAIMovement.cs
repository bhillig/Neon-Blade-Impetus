using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeadAIMovement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject bullet;
    [SerializeField] private EnemyEntityCore core;
    [SerializeField] private HeadAIProfile AiProfile;

    private Transform player;
    private float alertTimer;
    private Vector3 turnPoint;
    private bool turnPointSet = false;
    private bool playerSpotted = false;
    private float reloadTimer;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        core.OnRespawn += Respawn;
    }

    private void OnDestroy()
    {
        core.OnRespawn -= Respawn;
    }

    public bool GetPlayerSpotted()
    {
        return playerSpotted;
    }

    void Update()
    {
        if (!core.IsDead && Vector3.Distance(this.transform.position, player.position) <= AiProfile.ActivationRange)
        {
            if (!playerSpotted)
            {
                if (Physics.Raycast(barrel.transform.position, 
                    barrel.transform.position, 
                    AiProfile.ScopeRange, 
                    AiProfile.WhatIsPlayer))
                {
                    Shoot();
                    playerSpotted = true;
                    alertTimer = AiProfile.AlertTime;
                }

                if (Vector3.Distance(this.transform.position, player.position) <= AiProfile.SightRange)
                {
                    playerSpotted = true;
                    alertTimer = AiProfile.AlertTime;
                    reloadTimer = AiProfile.ReloadDelay;
                }

                else 
                {
                    TurnRandom();
                }
            }

            else
            {
                TurnLockOn();
                if (reloadTimer <= 0) 
                {
                    reloadTimer = AiProfile.ReloadDelay;
                    Shoot();
                }
                else
                {
                    reloadTimer -= Time.deltaTime;
                }
                if (alertTimer <= 0)
                {
                    Reset();
                }
                else
                {
                    alertTimer -= Time.deltaTime;
                }

            }
        }
        
    }

    public void Respawn()
    {
        reloadTimer = 0f;
        AiProfile.AlertTime = 0f;
    }

    void Shoot()
    {
        var newBullet = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
        var controller = newBullet.GetComponent<BulletController>();
        controller.SetTarget(player);
        controller.SetTargetHeightOffset(AiProfile.TargetHeightOffset);
        controller.SetStartDirection(barrel.transform.forward);
    }

    void TurnLockOn()
    {
        turnPoint = player.position;
        turnPointSet = true;
        Vector3 lookPos = transform.position - turnPoint;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, AiProfile.RotationSpeed * Time.deltaTime);
    }

    void TurnRandom()
    {
        if(!turnPointSet) 
        {
            float x = Random.Range(25f, 180f);
            float y = Random.Range(25f, 180f);
            float z = Random.Range(0f, 359f);
            turnPoint = new Vector3(x,y,z);
            turnPointSet = true;
        }

        else
        {
            Vector3 lookPos = turnPoint - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, AiProfile.RotationSpeed);

            if (transform.rotation == rotation)
            {
                turnPointSet = false;
            }
        }
    }

    void Reset()
    {
        playerSpotted = false;
    }

    public bool Activate()
    {
        return Vector3.Distance(this.transform.position, player.position) <= AiProfile.ActivationRange;
    }

}
