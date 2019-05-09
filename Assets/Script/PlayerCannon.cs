using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : MonoBehaviour
{
    [Header("Variabili Numeriche")]    
    public float moveDirX;
    public float moveDirZ;
    public float moveSpeed = 8.0f;
    public float projSpeed = 5000;
    [Header("GameObject")]
    public Rigidbody Proj;
    GameObject FirePoint;

    Vector3 moveDir = Vector3.zero;
   
    void Start()
    {
        FirePoint = GameObject.FindGameObjectWithTag("FirePoint");  
    }

   
    void Update()
    {
        moveDirX = Input.GetAxis("Horizontal");
        moveDirZ = Input.GetAxis("Vertical");
        moveDir = new Vector3(moveDirX, 0, moveDirZ);
        moveDir *= moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), moveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody projIstance;
            projIstance = Instantiate(Proj , FirePoint.transform.position, FirePoint.transform.rotation) as Rigidbody;
            projIstance.AddForce(FirePoint.transform.forward * projSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
