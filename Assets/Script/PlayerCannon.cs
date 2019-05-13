using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
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
    Camera mainCam;

    bool escape = false;

    void Start()
    {
        mainCam = FindObjectOfType<Camera>();
        FirePoint = GameObject.FindGameObjectWithTag("FirePoint");  
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

        if (escape == false)
        {
            Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if(groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                transform.LookAt(pointToLook);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody projIstance;
            projIstance = Instantiate(Proj, FirePoint.transform.position, FirePoint.transform.rotation) as Rigidbody;
            projIstance.AddForce(FirePoint.transform.forward * projSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
