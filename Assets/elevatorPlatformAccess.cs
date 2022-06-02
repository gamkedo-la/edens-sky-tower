using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorPlatformAccess : MonoBehaviour
{

    public GameObject frontWall;
    public ThirdPersonCameraController cameraScript;
    public TowerElevator towerElevScript;


    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("fronwall on");
            frontWall.SetActive(true);
            cameraScript.sharpTransition = true;
            towerElevScript.inMotion = true;

        }
    }
}
