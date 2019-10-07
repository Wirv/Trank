using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles_Destroy : MonoBehaviour
{

    void Update()
    {
        if(gameObject.tag != "Laser")
            Invoke("Destroyer", 1f);
        else
            Invoke("Destroyer", 0.6f);
    }

    public void Destroyer()
    {
        Destroy(gameObject);
    }
}
