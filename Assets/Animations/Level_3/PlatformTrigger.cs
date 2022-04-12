using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public GameObject platformMove;

    void OnTriggerEnter(Collider other)

    {
        if (other.tag == "Laser") 
            {
                platformMove.GetComponent<Animator>().Play("Platform");
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
