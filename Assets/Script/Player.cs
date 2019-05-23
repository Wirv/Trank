using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Variabili Numeriche")]
    public int Life = 100;
    public float verticalVelocity;
    public float moveDirX;
    public float moveDirZ;
    public float moveSpeed = 8.0f;
    public float rotationSpeed = 8.0f;

    
    Vector3 moveDir = Vector3.zero;

    [Header("Slider")]
    public Slider slider; // da mettere manualmente

    [Header("GameObject")]
    public GameObject particles; // da mettere manualmente

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
        moveDirX = Input.GetAxis("Horizontal");
        moveDirZ = Input.GetAxis("Vertical");
        moveDir = new Vector3(moveDirX,0,moveDirZ);
        moveDir *= moveSpeed * Time.deltaTime;
        transform.Translate(moveDir, Space.World);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir),rotationSpeed * Time.deltaTime);

        if (Life <= 0)           
            Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ProjectEn")
        {
            Life -= 5;
            Destroy(other.gameObject);
        }


        if (other.tag == "Explosion")
        {
            Life -= 50;
        }

    }

    private void OnDestroy()
    {
        Instantiate(particles, gameObject.transform.position , gameObject.transform.rotation);
    }
}
