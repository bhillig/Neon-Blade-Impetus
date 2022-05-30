using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float destructTime;
    [SerializeField] private float trackingAcceleration;
    [SerializeField] private float moveSpeed;

    private Transform player;
    private Rigidbody rb;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        ProjectileMove(moveSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        //this.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(player.position.x, player.position.y+1, player.position.z), moveSpeed*Time.smoothDeltaTime);
        Invoke("DestroyBullet", destructTime);
    }

    private void FixedUpdate()
    {
        float accelStep = trackingAcceleration * Time.deltaTime;
        ProjectileMove(accelStep);
        this.gameObject.transform.forward = rb.velocity;       
    }

    private void ProjectileMove(float accelStep)
    {
        Vector3 currentVel = rb.velocity;
        Vector3 desiredVel = (player.position + Vector3.up * 1.5f - transform.position).normalized * moveSpeed;
        Vector3 newVel = Vector3.MoveTowards(currentVel, desiredVel, accelStep);
        rb.velocity = newVel;
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerController>())
        {
            Debug.Log("HIT!");
            //Destroy(other.gameObject);
        }
        Destroy(this.gameObject);
    }
}
