using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Wall_Collision : MonoBehaviour
{
    Vector3 TempC;
    float TempH;
    Vector3 MaxC = new Vector3(0, 0, 50);
    float MaxH = 109.2f;

    private void Update()
    {
        if(gameObject.GetComponent<CapsuleCollider>().center.z < MaxC.z)
        {
            gameObject.GetComponent<CapsuleCollider>().center += new Vector3(0,0,240 * Time.deltaTime);
            TempC = gameObject.GetComponent<CapsuleCollider>().center;
        }

        if(gameObject.GetComponent<CapsuleCollider>().height < MaxH)
        {
            gameObject.GetComponent<CapsuleCollider>().height += 520 * Time.deltaTime;
            TempH = gameObject.GetComponent<CapsuleCollider>().height;
        }
       
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Wall")
        {
            MaxC = TempC;
            MaxH = TempH;
        }
    }
}
