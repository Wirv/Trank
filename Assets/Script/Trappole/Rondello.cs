using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rondello : MonoBehaviour
{
    float y;

    // Update is called once per frame
    void Update()
    {
        y += Time.deltaTime * 25;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }
}
