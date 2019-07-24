using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Wall")
            Destroy(gameObject);


    }
    
}
