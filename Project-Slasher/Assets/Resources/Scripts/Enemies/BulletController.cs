using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float destructTime;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;

    private Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3 (0, 30, 0), rotationSpeed*Time.deltaTime, Space.World);
        this.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(player.position.x, player.position.y+1, player.position.z), moveSpeed*Time.smoothDeltaTime);
        Invoke("DestroyBullet", destructTime);
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
        Destroy(this.gameObject);
    }
}
