using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public static Boss bossScript;

    public GameObject ShieldForce , AnelloProtettivo, BaseCannone, Cannone, FacciataD, FacciataS , player , PuntoDiFuoco ,ProjectBoss, particles, LifeBar;

    public Slider BarraVita;

    AudioSource audiosrc;

    public bool fightOn, InFight, shot;

    public int life;

    private float speed;

    private void Awake()
    {
        audiosrc = GetComponent<AudioSource>();
        life = 500;
        speed = 10f;
        bossScript = this;
        fightOn = false;
        InFight = false;
        shot = false;
        ShieldForce.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        BarraVita.maxValue = life;
    }

    // Update is called once per frame
    void Update()
    {
        if(fightOn == true)
        {
            

            if(InFight == true)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(0,4.9f,121.1f), speed * Time.deltaTime);
                BaseCannone.transform.localPosition = Vector3.MoveTowards(BaseCannone.transform.localPosition, new Vector3(2,0,2), speed * Time.deltaTime);
                Cannone.transform.localPosition = Vector3.MoveTowards(Cannone.transform.localPosition, new Vector3(2, 0, 3.791979f), speed * Time.deltaTime);
                AnelloProtettivo.transform.localRotation = Quaternion.RotateTowards(AnelloProtettivo.transform.localRotation, Quaternion.Euler(-90f,0,0), 600 * Time.deltaTime);
                ShieldForce.SetActive(false);
                FacciataD.transform.localPosition = Vector3.MoveTowards(FacciataD.transform.localPosition, new Vector3(-2, 0, 2), speed * Time.deltaTime);
                FacciataS.transform.localPosition = Vector3.MoveTowards(FacciataS.transform.localPosition, new Vector3(2, 0, 2), speed * Time.deltaTime);

                gameObject.transform.LookAt(player.transform.position);
                if(shot == false)
                StartCoroutine(Shot());
            }
            else
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(0, 40f, 121.1f), speed * Time.deltaTime);
                BaseCannone.transform.localPosition = Vector3.MoveTowards(BaseCannone.transform.localPosition, new Vector3(2, 0, -0.58f), speed * Time.deltaTime);
                Cannone.transform.localPosition = Vector3.MoveTowards(Cannone.transform.localPosition, new Vector3(2, 0, -0.32f), speed * Time.deltaTime);                
                FacciataD.transform.localPosition = Vector3.MoveTowards(FacciataD.transform.localPosition, new Vector3(0, 0, 0), speed * Time.deltaTime);
                FacciataS.transform.localPosition = Vector3.MoveTowards(FacciataS.transform.localPosition, new Vector3(0, 0, 0), speed * Time.deltaTime);
                AnelloProtettivo.transform.Rotate(new Vector3(1,0,0), 600 * Time.deltaTime);
                ShieldForce.SetActive(true);
            }
        }

        BarraVita.value = life;

        if (life <= 0)
        {
            Destroy(gameObject);
        }

    }

    public IEnumerator Shot()
    {
        shot = true;
        yield return new WaitForSeconds(0.8f);
        Rigidbody projIstance;
        audiosrc.Play();
        projIstance = Instantiate(ProjectBoss.GetComponent<Rigidbody>(), PuntoDiFuoco.transform.position, PuntoDiFuoco.transform.rotation);
        projIstance.transform.localScale = new Vector3(10, 10, 10);
        projIstance.AddForce(PuntoDiFuoco.transform.forward * 4000 * Time.deltaTime, ForceMode.Impulse);
        shot = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Project" || other.tag == "ProjectAP")
        {
            if (fightOn == false)
            {
                if(LifeBar != null)
                {
                    LifeBar.SetActive(true);
                }
                fightOn = true;
            }

            if(fightOn == true)
            {
                if (other.tag == "Project")
                    life -= 8;
                else
                    life -= 14;
            }
        }
    }

    private void OnDestroy()
    {
        if (Application.isPlaying && !Application.isLoadingLevel)
        {
            Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
        }

    }
}
