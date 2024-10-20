using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawmove : MonoBehaviour
{
    private float speed = 33f;
    
    void Update()
    {
        transform.Rotate(0, 0, 360 * speed * Time.deltaTime);
    }
}
