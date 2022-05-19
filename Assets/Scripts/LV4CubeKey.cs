using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV4CubeKey : MonoBehaviour
{
    public Player playerScript; // referencing Player script

    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        playerScript.carryingLV4CubeKey = true;
    }
}
