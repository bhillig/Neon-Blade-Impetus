using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem ps;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.ps.isStopped)
        {
            this.timer += Time.deltaTime;
            if(this.timer > 1.0f)
            {
                Debug.Log("Destroyed particle");
                Destroy(gameObject);
                Destroy(this);
            }          
        }
    }
}
