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
    protected int life = 12;
    protected int maxlife = 12;
    protected float maxAngle = 25;
    protected float maxRadius = 120;
    [Header("Variabili Booleane")]
    public bool ready = false;
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
    public List<Transform> waypoint = new List<Transform>();
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
                    if(notFound == false)
                        Invoke("StopFollow", 5);
                }

            }
        }
    }

    public void StopFollow()
    {
        transform.LookAt(null);
        agent.enabled = true;
        takePlayer = false;
        agent.speed = speed;
        notFound = true;
    }
    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        slider.maxValue = life;
        HealtBarUI.SetActive(false);
        int i = 1;
        while (ready == false)
        {
            if (GameObject.FindGameObjectWithTag("Point" + i) != null)
            {
                waypoint.Add(GameObject.FindGameObjectWithTag("Point" + i).transform);
                i++;
            }
            else
                ready = true;


        }
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

        if (takePlayer == false && takePoint == false)
        {
            int I = UnityEngine.Random.Range(0, waypoint.Count);
            target = waypoint[I];
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
                transform.Translate(Vector3.forward * speed * 3 * Time.deltaTime);
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
        if (other.name == target.name)
            takePoint = false;

        if (other.tag == "Project")
        {
            takePlayer = true;
            life -= 8;

            Destroy(other.gameObject);
        }

        if(other.name == "Player")
        {
            explosion = true;
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            Player.Life -= 50;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(explosion == false)
        Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
    }
}
