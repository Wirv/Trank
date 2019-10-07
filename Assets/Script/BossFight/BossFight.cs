using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public static BossFight bossfight;

    public GameObject vita1, vita2, spada, scudo;

    public GameObject[] cristalli;

    public GameObject[] Spawns;

    public bool vit, spd, scd, cristallib;

    private bool enemySpawn;

    [Header("NemiciSpawnabili")]
    public GameObject enemy;
        public GameObject enemyLeg;

    private float timer = 0, tempTimer = 0, tempTimer2 = 0, tempTimer3 = 0 , timercristalli = 0;

    public int i;

    private void Awake()
    {
        vit = false; spd = false; scd = false; cristallib = false;
        enemySpawn = false;
        bossfight = this;
        cristalli = GameObject.FindGameObjectsWithTag("Crist");
        Spawns = GameObject.FindGameObjectsWithTag("Spawn");
        timer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        #region timer
        timer += Time.deltaTime;

        if(vit == true)
        {
            tempTimer = timer + 40f;
            vit = false;
        }

        if(timer >= tempTimer && !vita1.activeInHierarchy)
        {

            vita1.SetActive(true);

        }
        else if (timer >= tempTimer && !vita2.activeInHierarchy)
        {

            vita2.SetActive(true);

        }


        if(spd == true)
        {
            tempTimer2 = timer + 25f;
            spd = false;
        }

        if (timer >= tempTimer2 && !spada.activeInHierarchy)
        {

            spada.SetActive(true);

        }
        
        if(scd == true)
        {
            tempTimer3 = timer + 30f;
            scd = false;
        }

        if (timer >= tempTimer3 && !scudo.activeInHierarchy)
        {

            scudo.SetActive(true);

        }
        #endregion

        
        foreach(var x in cristalli)
        {
            if (x.GetComponent<CristallBoss>().changed == true)
                i++;
            else
            {
                i = 0;
                
            }

            if(i == cristalli.Length && cristallib == false)
            {
                Boss.bossScript.InFight = true;
                timercristalli = timer + 30f;
                cristallib = true;
                i = 0;
            }

        }

        if(timer >= timercristalli && cristallib == true)
        {
            foreach(var y in cristalli)
            {
                y.GetComponent<CristallBoss>().GetComponent<MeshRenderer>().material = y.GetComponent<CristallBoss>().main;
                y.GetComponent<CristallBoss>().changed = false;
                
            }
            Boss.bossScript.InFight = false;
            cristallib = false;
        }


        if(Boss.bossScript.InFight == false && Boss.bossScript.fightOn == true)
        {
            Debug.Log("primo");
            if (enemySpawn == false)
                StartCoroutine(Spawner());

        }

        
    }

    private IEnumerator Spawner()
    {
        enemySpawn = true;
        
        yield return new WaitForSeconds(5f);

        switch (Random.Range(0, 2))
        {
            case 0:
                int r = Random.Range(0, Spawns.Length);
                Instantiate(enemy, Spawns[r].transform.position, Spawns[r].transform.rotation);
                break;

            case 1:
                int r2 = Random.Range(0, Spawns.Length);
                Instantiate(enemyLeg, Spawns[r2].transform.position, Spawns[r2].transform.rotation);
                break;
        }
        enemySpawn = false;

    }
}
