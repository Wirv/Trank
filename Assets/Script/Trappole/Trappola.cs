using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trappola : MonoBehaviour
{
    public GameObject[] enmy;

    public GameObject giu;

    public bool active = false;

    float giuok;

    private void Start()
    {
        active = false;
        giuok = giu.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(active == true && enmy[0] != null && enmy[1] != null && enmy[2] != null)
        {
           
            if(giu.transform.position.y >= giuok - 50)
            giu.transform.Translate(Vector3.down * 35 * Time.deltaTime);



            if(giu.transform.position.y <= giuok - 50 && enmy.Length > 0)
            for (int i = 0; i < enmy.Length; i++)
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
