using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV4CubeKey : MonoBehaviour
{
    public Player playerScript; // referencing Player script
    public GameObject RotatingObject;

    void Update ()
    {
        RotatingObject.transform.Rotate(0.05f, 0.05f, 0.05f, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        playerScript.carryingLV4CubeKey = true;
    }
}
