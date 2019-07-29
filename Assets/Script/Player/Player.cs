using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MobileJoystick;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Joystick")]
    protected Joystick Joystick;


    [Header("Variabili Numeriche")]
    public int Life = 100;
    public float verticalVelocity;
    public float moveSpeed = 60;
    public float rotationSpeed = 8.0f;

    
    Vector3 moveDir = Vector3.zero;

    [Header("Slider")]
    public Slider slider; // da mettere manualmente

    [Header("GameObject")]
    public GameObject particles; // da mettere manualmente

    void Awake()
    {
        Joystick = FindObjectOfType<Joystick>();
        slider = GameObject.FindGameObjectWithTag("UIP").GetComponent<Slider>();
    }

    public void Start()
    {      
        slider.maxValue = Life;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Life;
        Vector3 temp = transform.position;
        temp.y = 2.45f;
        transform.position = temp;
        transform.position = new Vector3(transform.position.x + Joystick.AxisNormalized.x * moveSpeed * Time.deltaTime, temp.y, transform.position.z + Joystick.AxisNormalized.y * moveSpeed * Time.deltaTime);
        moveDir = new Vector3(Joystick.AxisNormalized.x, 0, Joystick.AxisNormalized.y);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), rotationSpeed * Time.deltaTime);

        if (Life <= 0)           
            Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ProjectEn")
        {
            Life -= 8;
            Destroy(other.gameObject);
        }

        if (other.tag == "ProjectPS")
        {
            Life -= 10;
            Destroy(other.gameObject);
        }

        if (other.tag == "Laser")
        {
            Life -= 20;
            
        }

        

    }

    private void OnDestroy()
    {
        if (Application.isPlaying && !Application.isLoadingLevel)
            Instantiate(particles, gameObject.transform.position , gameObject.transform.rotation);
    }
}
