using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeadAIMovement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject bullet;
    [SerializeField] private AbstractEnemyEntity core;
    [SerializeField] private HeadAIProfile AiProfile;
    [SerializeField] private FMODUnity.StudioEventEmitter shootVFX;

    [SerializeField] private float sightModifier = 1;

    private Transform player;
    private float alertTimer;
    private Vector3 turnPoint;
    private Vector3 lookPos;
    private bool turnPointSet = false;
    private bool playerSpotted = false;
    private float reloadTimer;
    private Quaternion startRotation;
    
    void Awake()
    {     
        core.OnRespawn += Respawn;
        startRotation = transform.rotation;
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

                if (Vector3.Distance(this.transform.position, player.position) <= AiProfile.SightRange*sightModifier)
                {
                    playerSpotted = true;
                    alertTimer = AiProfile.AlertTime;
                    reloadTimer = AiProfile.ReloadDelay;
                }

                else 
                {
                    //TurnRandom();
                }
            }

            else
            {
                TurnLockOn();
                float dot = Vector3.Dot(barrel.transform.forward, lookPos.normalized);
                if (reloadTimer <= 0 && dot > 0.9f) 
                {
                    reloadTimer = AiProfile.ReloadDelay;
                    Shoot();
                }
                else
                {
                    if(Vector3.Distance(this.transform.position, player.position) >= AiProfile.ShowdownRange) 
                    {
                        reloadTimer -= Time.deltaTime;
                    }
                    else
                    {
                        reloadTimer -= Time.deltaTime/2;
                    }
                }
                if (alertTimer <= 0)
                {
                    //Reset();
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
        transform.rotation = startRotation;
    }

    void Shoot()
    {
        shootVFX?.Play();
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
        lookPos = turnPoint - transform.position + player.up * AiProfile.TargetHeightOffset;
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
