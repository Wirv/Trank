using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {


        if (other.name != "Sphere") 
            if (other.tag == "Wall")
                Destroy(gameObject); 
            
        
    }
}
