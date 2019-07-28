using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OggettiTrappola : MonoBehaviour
{
    Trappola trap;

    // Start is called before the first frame update
    void Start()
    {
        trap = FindObjectOfType<Trappola>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            trap.active = true;
        }
    }
}
