using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotKiller : MonoBehaviour
{
   void OnTriggerEnter(Collider other) 
   {
       if (other.tag == "Player") 
       {
           Destroy(this.gameObject);
       }
   }
}
