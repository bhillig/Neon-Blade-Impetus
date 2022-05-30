using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeadAIMovement : MonoBehaviour
{
    private Transform player;
    [SerializeField] private LayerMask whatIsPlayer;
    private Vector3 turnPoint;
    private bool turnPointSet = false;
    private bool playerSpotted = false;

    [SerializeField] private GameObject barrel;

    [SerializeField] private float sightRange;

    [SerializeField] private float activationRange;

    [SerializeField] private float scopeRange;

    [SerializeField] private float reloadDelay;

    [SerializeField] private float alertTime;

    [SerializeField] private float lookSpeed;

    [SerializeField] private float targetHeightOffset;

    [SerializeField] GameObject bullet;

    [SerializeField] private EnemyEntityCore core;

    private float alertTimer;

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
        if (!core.IsDead && Vector3.Distance(this.transform.position, player.position) <= activationRange)
        {
            if (!playerSpotted)
            {
                if (Physics.Raycast(barrel.transform.position, barrel.transform.position, scopeRange, whatIsPlayer))
                {
                    Shoot();
                    playerSpotted = true;
                    alertTimer = alertTime;
                }

                if (Vector3.Distance(this.transform.position, player.position) <= sightRange)
                {
                    playerSpotted = true;
                    alertTimer = alertTime;
                    reloadTimer = reloadDelay;
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
                    reloadTimer = reloadDelay;
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
        alertTime = 0f;
    }

    void Shoot()
    {
        var newBullet = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
        var controller = newBullet.GetComponent<BulletController>();
        controller.SetTarget(player);
        controller.SetTargetHeightOffset(targetHeightOffset);
    }

    void TurnLockOn()
    {
        turnPoint = player.position;
        turnPointSet = true;
        Vector3 lookPos = turnPoint - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed);
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
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed);

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
        return Vector3.Distance(this.transform.position, player.position) <= activationRange;
    }

}
