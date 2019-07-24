using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPlayer : MonoBehaviour
{
    [Header("metti a mano")]
    public GameObject die; // mettere a mano
    public GameObject victory; // mettere a mano

    [Header("Altro")]
    public GameObject player;

    public GameObject[] Enemy;
    public GameObject[] EnemyLeg;
    public GameObject[] EnemyPes;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyLeg = GameObject.FindGameObjectsWithTag("EnemyLeg");
        EnemyPes = GameObject.FindGameObjectsWithTag("EnemyPes");
    }

    private void Start()
    {
        die.SetActive(false);
        victory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            die.SetActive(true);
        }

        if(Enemy.Length == 0 && EnemyLeg.Length == 0 && EnemyPes.Length == 0)
        {
            victory.SetActive(true);

            if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                Application.Quit();
            }
        }

    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quitting()
    {
        Application.Quit();
    }

    public void Research()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyLeg = GameObject.FindGameObjectsWithTag("EnemyLeg");
        EnemyPes = GameObject.FindGameObjectsWithTag("EnemyPes");
    }

    public void Continue()
    {
        int nextscene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextscene);
    }
}
