using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public Transform cameraPositionOverride;
    public ThirdPersonCameraController cameraScript;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (cameraPositionOverride)
            {
                cameraScript.sharpTransition = true;
                ThirdPersonCameraController.instance.cameraPositionOverride = cameraPositionOverride;
            }
        }
    }

    /*
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cameraScript.sharpTransition = false;
            ThirdPersonCameraController.instance.cameraPositionOverride = null;
        }
    }
    */
}
