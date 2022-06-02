using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatform : MonoBehaviour
{
    public Transform cameraPositionOverride;
    public ThirdPersonCameraController cameraScript;
    public GameObject startingPoint;
    ////public GameObject frontWall;
    public LevelTowerSelection LevelTowerSelectionScript;
    public GameObject platformModel;
    public GameObject platformResetPoint;
    public bool resetElevPlatform = false;

    void Update ()
    {
        if(resetElevPlatform == true)
        {
            platformModel.transform.position = platformResetPoint.transform.position;
            resetElevPlatform = false;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            //if (cameraPositionOverride)
           // {
                ////cameraScript.sharpTransition = true;
                ThirdPersonCameraController.instance.cameraPositionOverride = cameraPositionOverride;
                ////frontWall.SetActive(true);
                LevelTowerSelectionScript.selectedLevel = false;
           // }
        }
    }

 
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("normal camera view");
            cameraScript.sharpTransition = false;
            ThirdPersonCameraController.instance.cameraPositionOverride = null;            
        }
    }

}
