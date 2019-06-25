using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trappola : MonoBehaviour
{
    public GameObject[] enmy;

    public bool active = false;

    // Update is called once per frame
    void Update()
    {
        if(active == true)
        {
            for(int i = 0; i < enmy.Length - 1; i++)
            {
                switch (enmy[i].tag)
                {
                    case "Enemy":
                        enmy[i].GetComponent<Enemy>().takePlayer = true;
                        break;
                    case "EnemyLeg":
                        enmy[i].GetComponent<Enemy_Leg>().takePlayer = true;
                        break;
                    case "EnemyPes":
                        enmy[i].GetComponent<Enemy_Pesante>().takePlayer = true;
                        break;

                }
            }
        }
    }
}
