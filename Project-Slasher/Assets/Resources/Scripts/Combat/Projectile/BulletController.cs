using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BulletController : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField] private float destructTime;
    [SerializeField] private float trackingAcceleration;
    [SerializeField] private float moveSpeed;
    private Transform player;
    private Collider coll;
    private Rigidbody rb;
    private bool destroyed = false;
    private float targetHeightOffset;

    [Header("Renderers")]
    [SerializeField] private Renderer bulletRenderer;

    [Header("VFX")]
    [SerializeField] private List<ParticleSystem> impactParticles;
    [SerializeField] private List<ParticleSystem> trailParticles;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void Start()
    {
        ProjectileMove(moveSpeed);
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }

    public void SetTargetHeightOffset(float val)
    {
        targetHeightOffset = val;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(player.position.x, player.position.y+1, player.position.z), moveSpeed*Time.smoothDeltaTime);
        Invoke("DestroyBullet", destructTime);
    }

    private void FixedUpdate()
    {
        if (!destroyed)
        {
            float accelStep = trackingAcceleration * Time.deltaTime;
            ProjectileMove(accelStep);
        }    
    }

    private void ProjectileMove(float accelStep)
    {
        Vector3 currentVel = rb.velocity;
        Vector3 desiredVel = (player.position + Vector3.up * targetHeightOffset - transform.position).normalized * moveSpeed;
        Vector3 newVel = Vector3.MoveTowards(currentVel, desiredVel, accelStep);
        rb.velocity = newVel;
        this.gameObject.transform.forward = rb.velocity;
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (destroyed)
            return;
        var player = collision.gameObject.GetComponentInParent<PlayerHitboxManager>();
        if (player != null)
        {
            player.HitByProjectile();
        }
        OnImpact();
    }

    private void OnImpact()
    {
        impactParticles.ForEach(particle => particle?.Play());
        trailParticles.ForEach(particle => particle?.Stop());
        rb.constraints = RigidbodyConstraints.FreezeAll;
        bulletRenderer.enabled = false;
        coll.enabled = false;
        destroyed = true;
    }
}
