using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV4CubeKey : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
