using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.tag == "Player") 
       {
           DestroyImmediate(this.gameObject);
       }
   }
}
