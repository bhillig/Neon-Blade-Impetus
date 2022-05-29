using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotKiller : MonoBehaviour
{
   void OnCollisionEnter(Collision other) 
   {
       if (other.gameObject.tag == "Player") 
       {
           Destroy(this.gameObject);
       }
   }
}
