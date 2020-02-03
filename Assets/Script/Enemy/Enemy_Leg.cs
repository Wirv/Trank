using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_Leg : MonoBehaviour
{
    #region Variabili
    [Header("Variabili Numeriche")]
    public float speed = 20;
    protected int life = 10;
    protected int maxlife = 10;
    protected float maxAngle = 25;
    protected float maxRadius = 120;
    [Header("Variabili Booleane")]
    public bool NoTakeWay = false;
    public bool Trap = false;
    public bool takePoint = false;
    public bool takePlayer = false;
    public bool explosion = false;
    public bool notFound = false;
    [Header("Variabili Miste")]
    public Transform player;
    public Player Player;
    public Transform target;
    public NavMeshAgent agent;
    public GameObject HealtBarUI; // da mettere manualmente
    public Slider slider;// da mettere manualmente
    public Camera cameramain; // da mettere manualmente
    public GameObject particles; // da mettere manualmente
    public GameObject Explosion; // da mettere manualmente
    public GameObject[] waypoint;
    public UIPlayer uip;
    #endregion

    #region MiniFunc
    public void InFov()//Da Aggiustare
    {
        if (player)
        {
            Vector3 directionBetween = (player.position - transform.position).normalized;
            directionBetween.y *= 0;

            float angle = Vector3.Angle(transform.forward, directionBetween);

            if (angle <= maxAngle)
            {
                Ray ray = new Ray(transform.position, player.position - transform.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxRadius * 3))
                {
                    if (hit.transform == player)
                    {
                        takePlayer = true;

                    }
                }
                else
                {
                    if(notFound == false && Trap == false)
                        StartCoroutine(StopFollow());
                }

            }
        }
    }

    public IEnumerator StopFollow()
    {
        notFound = true;
        yield return new WaitForSeconds(5f);
        takePlayer = false;
        notFound = false;
    }
    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        cameramain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        uip = FindObjectOfType<UIPlayer>();
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        slider.maxValue = life;
        HealtBarUI.SetActive(false);
        waypoint = GameObject.FindGameObjectsWithTag("Point");
    }

    public void Update()
    {
        slider.value = life;
        slider.transform.LookAt(cameramain.transform.position);
        InFov();

        if (life < maxlife)
        {
            HealtBarUI.SetActive(true);
        }

        if (takePlayer == false && takePoint == false && NoTakeWay == false)
        {
            int I = UnityEngine.Random.Range(0, waypoint.Length - 1);
            target = waypoint[I].transform;
            agent.SetDestination(target.position);
            takePoint = true;
        }

        if (takePlayer == true)
        {
            takePoint = false;
            if (player)
            {
                target = player;
                agent.enabled = false;
                transform.LookAt(target.position);
                transform.Translate(Vector3.forward * speed * 5 * Time.deltaTime);
            }
        }



        if (life <= 0)
            Destroy(gameObject);

    }

    private void OnDrawGizmos() // da ricercare , crea una zona di trigger
    {
        Gizmos.color = Color.magenta; // crea la zona in questo caso una sfera di trigger
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * (maxRadius * 3); // linee di visuale
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * (maxRadius * 3);

        Gizmos.color = Color.magenta; // linee visive sulla scena
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        if (player)
        {
            if (!takePlayer)
                Gizmos.color = Color.cyan; // ray che segue il player, rimanendo nella zona dell'enemy
            else
                Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);
        }
        Gizmos.color = Color.magenta; // ray centrale
        Gizmos.DrawRay(transform.position, transform.forward * (maxRadius * 3));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (target != null)
        {
            if (other.name == target.name)
                takePoint = false;
        }

        if (other.tag == "Project")
        {
            takePlayer = true;
            life -= 10;

            Destroy(other.gameObject);
        }
        else if (other.tag == "ProjectAP")
        {
            takePlayer = true;
            life -= 12;

            Destroy(other.gameObject);
        }

        if (other.name == "Player" && !Player.ShieldForce.activeInHierarchy)
        {
            explosion = true;
         
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            Player.Life -= 25;
            Destroy(gameObject);
        }

        if (other.tag == "Wall" && takePlayer == true)
        {
            explosion = true;
           
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        
        if (Application.isPlaying && !Application.isLoadingLevel)
        {
            uip.Research();
            if (explosion == false)
            {
                Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
                
            }
                
        }
           
    }
}
