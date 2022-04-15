using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public GameObject platformMove;

    private bool beenTriggered = false;

    public void ActivatePlatform()
    {
        if (beenTriggered)
        {
            return;
        }
        beenTriggered = true;
        platformMove.GetComponent<Animator>().Play("Platform");
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
