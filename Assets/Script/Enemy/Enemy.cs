using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region Variabili
    [Header("Variabili Numeriche")]
    public float speed = 20;
    float projSpeed = 5000;
    protected int life = 30;
    protected int maxlife = 30;
    public float maxAngle = 45;
    float maxRadius = 60;
    [Header("Variabili Booleane")]
    public bool NoTakeWay = false;
    public bool Trap = false;
    public bool takePoint = false;
    public bool takePlayer = false;
    public bool shot = false;
    public bool notFound = false;
    [Header("Variabili Miste")]
    public Transform player;
    public GameObject Proj; // variabile da mettere manualmente
    public GameObject TestaCannone; //da mettere manualmente
    public GameObject FirePoint; // da mettere manualmente
    public Transform target;
    public NavMeshAgent agent;
    public GameObject HealtBarUI; // da mettere manualmente
    public Slider slider;// da mettere manualmente
    public Camera cameramain; // da mettere manualmente
    public GameObject particles; // da mettere manualmente
    public GameObject[] waypoint;
    public UIPlayer uip;
    AudioSource audiosrc;
    #endregion


    //public static bool InFov (Transform checkingObject, Transform playerf, float maxAngle, float maxRadius)
    //{
    //    Collider[] overlaps = new Collider[10];
    //    int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);
    //    for (int i = 0; i < count ; i++)
    //    {
    //        if(overlaps[i] != null)
    //        {
    //            if(overlaps[i].transform == playerf) 
    //            {
    //                Vector3 directionBetween = (playerf.position - checkingObject.position).normalized;
    //                directionBetween.y *= 0;

    //                float angle = Vector3.Angle(checkingObject.forward, directionBetween);

    //                if (angle <= maxAngle)
    //                {
    //                    Ray ray = new Ray(checkingObject.position, playerf.position - checkingObject.position);
    //                    RaycastHit hit;

    //                    if (Physics.Raycast(ray, out hit, maxRadius))
    //                    {
    //                        if (hit.transform == playerf)
    //                            return true;
    //                    }

    //                }
    //                else
    //                    return false;
    //            }
    //        }
    //    }
    //    return false;
    //}
    #region MiniFunc
    public void InFov()//Da Aggiustare
    {
        if (player)
        {
            Vector3 directionBetween = (player.position - transform.position).normalized;
            directionBetween.y *= 0;

            float angle = Vector3.Angle(TestaCannone.transform.forward, directionBetween);

            if (angle <= maxAngle)
            {
                Ray ray = new Ray(transform.position, player.position - transform.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, maxRadius))
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
        TestaCannone.transform.LookAt(null);
        TestaCannone.transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation, Time.deltaTime);
        notFound = false;
    }
    public IEnumerator Shot()
    {
        shot = true;
        yield return new WaitForSeconds(0.5f);
        Rigidbody projIstance;
        audiosrc.Play();
        projIstance = Instantiate(Proj.GetComponent<Rigidbody>(), FirePoint.transform.position, FirePoint.transform.rotation);
        projIstance.AddForce(FirePoint.transform.forward * projSpeed * Time.deltaTime, ForceMode.Impulse);
        shot = false;
    }
    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        uip = FindObjectOfType<UIPlayer>();

        audiosrc = GetComponent<AudioSource>();
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
        
        if(life < maxlife)
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
                agent.SetDestination(target.position);
                TestaCannone.transform.LookAt(target.position);
                if (shot == false)
                    StartCoroutine(Shot());
            }
        }

       

        if (life <= 0)
            Destroy(gameObject);

    }

    private void OnDrawGizmos() // da ricercare , crea una zona di trigger
    {
        Gizmos.color = Color.blue; // crea la zona in questo caso una sfera di trigger
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, TestaCannone.transform.up) * TestaCannone.transform.forward * maxRadius; // linee di visuale
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, TestaCannone.transform.up) * TestaCannone.transform.forward * maxRadius;

        Gizmos.color = Color.blue; // linee visive sulla scena
        Gizmos.DrawRay(TestaCannone.transform.position, fovLine1);
        Gizmos.DrawRay(TestaCannone.transform.position, fovLine2);

        if (player)
        {
            if (!takePlayer)
                Gizmos.color = Color.cyan; // ray che segue il player, rimanendo nella zona dell'enemy
            else
                Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);
        }
        Gizmos.color = Color.blue; // ray centrale
        Gizmos.DrawRay(TestaCannone.transform.position, TestaCannone.transform.forward * maxRadius);

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

    }

    private void OnDestroy()
    {
        Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
        uip.Research();
    }
}
