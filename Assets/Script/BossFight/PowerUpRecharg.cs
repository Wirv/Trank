using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRecharg : MonoBehaviour
{

    float y;

    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        y += Time.deltaTime * 50;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            switch (gameObject.name)
            {
                case "Health":

                    player.Life = 100;
                    
                    BossFight.bossfight.vit = true;
                    gameObject.SetActive(false);

                    break;

                case "Scudo":

                    player.ShieldForce.SetActive(true);
                    BossFight.bossfight.scd = true;
                    gameObject.SetActive(false);
                    break;

                case "Spada":

                    PlayerCannon.attackPlus = true;
                    BossFight.bossfight.spd = true;
                    gameObject.SetActive(false);

                    break;


            }
        }
    }
}
