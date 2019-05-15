using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles_Destroy : MonoBehaviour
{

    void Update()
    {
        Invoke("Destroyer", 3);
    }

    public void Destroyer()
    {
        Destroy(gameObject);
    }
}
