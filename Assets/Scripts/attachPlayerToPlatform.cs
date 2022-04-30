using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachPlayerToPlatform : MonoBehaviour
{
    public GameObject Avatar;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Avatar)
        {
            Avatar.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Avatar)
        {
            Avatar.transform.parent = null;
        }
    }
}
