using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser_Wall_Collision : MonoBehaviour
{
    private LineRenderer laser;

    RaycastHit hit;

    private bool hitting;

    public float lange;

    private void Start()
    {
        hitting = false;
        laser = gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {

        if (Physics.Raycast(laser.transform.position, laser.transform.TransformDirection(Vector3.forward), out hit, lange))
        {
            if (hit.collider.tag == "Wall" || hit.collider.tag == "Player")
            {
                laser.SetPosition(1, new Vector3(0, 0, hit.distance + 2f));
            }

            if(hit.collider.tag == "Player" && hitting == false)
            {
                hit.collider.gameObject.GetComponent<Player>().Life -= 20;
                hitting = true;
            }

        }
        else
        {
            laser.SetPosition(1, new Vector3(0, 0, lange));
        }


    }


    
}
