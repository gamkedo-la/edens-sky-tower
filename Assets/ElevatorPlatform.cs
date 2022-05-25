using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public Transform cameraPositionOverride;
    public ThirdPersonCameraController cameraScript;
    public GameObject startingPoint;
    public GameObject frontWall;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (cameraPositionOverride)
            {
                cameraScript.sharpTransition = true;
                ThirdPersonCameraController.instance.cameraPositionOverride = cameraPositionOverride;
                frontWall.SetActive(true);
            }
        }
    }

 
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("normal camera view");
            cameraScript.sharpTransition = false;
            ThirdPersonCameraController.instance.cameraPositionOverride = null;
            this.transform.position = startingPoint.transform.position;
        }
    }

}
