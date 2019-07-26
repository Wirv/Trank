using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MobileJoystick;

public class PlayerCannon : MonoBehaviour
{
    [Header("joystick")]
    protected Joystick_torretta Joystick;

    [Header("Variabili Numeriche")]    
    public float moveSpeed = 8.0f;
    public float projSpeed = 5000;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    public float minAngle = -60.0f;
    public float maxAngle = 70.0f;
    [Header("GameObject")]
    public Rigidbody Proj;
    GameObject FirePoint;
    AudioSource audiosrc;

    Vector3 moveDir = Vector3.zero;

    bool escape = false;
    bool shott = false;

    void Awake()
    {
        Joystick = FindObjectOfType<Joystick_torretta>();

        audiosrc = GetComponent<AudioSource>();
    }

    void Start()
    {
        FirePoint = GameObject.FindGameObjectWithTag("FirePoint");
        shott = false;
        escape = false;
    }

   
    void Update()
    {
        /////////////////////////////////////////////////////// Movimento Visuale /////////////////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.Escape) && escape == false) // stoppa tutto attivando una boleana
        {
            escape = true;
        }
        else if (Input.GetMouseButton(0) && escape == true)
        {
            escape = false;
        }

        if (escape == false && (Joystick.AxisNormalized.x != 0 || Joystick.AxisNormalized.y != 0))
        {

            moveDir = new Vector3(Joystick.AxisNormalized.x, 0, Joystick.AxisNormalized.y);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 10 * Time.deltaTime);

        }

        if (shott == false && (Joystick.AxisNormalized.x != 0 || Joystick.AxisNormalized.y != 0))
        {
            StartCoroutine(shot());
            shott = true;
        }
    }

    IEnumerator shot()
    {
        yield return new WaitForSeconds(0.5f);
        Rigidbody projIstance;
        audiosrc.Play();
        projIstance = Instantiate(Proj, FirePoint.transform.position, FirePoint.transform.rotation) as Rigidbody;
        projIstance.AddForce(FirePoint.transform.forward * projSpeed * Time.deltaTime, ForceMode.Impulse);
        shott = false;

    }
}
