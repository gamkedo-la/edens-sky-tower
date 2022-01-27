using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
   
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(Vector3.up,180.0f);
    }
}
