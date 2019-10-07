using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristallBoss : MonoBehaviour
{
    public static CristallBoss cristal;

    public Material main;

    public Material change;

    public bool changed;

    private void Awake()
    {
        changed = false;
        cristal = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        main = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Boss.bossScript.fightOn == true && (other.tag == "Project" || other.tag == "ProjectAP"))
        {
            gameObject.GetComponent<MeshRenderer>().material = change;
            changed = true;
            Destroy(other.gameObject);
        }
    }
}
