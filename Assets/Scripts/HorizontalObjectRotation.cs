using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObjectRotation : MonoBehaviour
{
    public GameObject controlledObject;
    bool controllingObject = false;
    Player playerScript;

    public towerBridgeMove towerBridgeMove;   
    public towerGateMove towerGateMove; 

    public Transform cameraPositionOverride;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(controllingObject) {
            if (Input.GetKey (KeyCode.A)) {
                controlledObject.transform.Rotate(0, 01, 0, Space.Self);
            }

            if (Input.GetKey (KeyCode.D)) {
                controlledObject.transform.Rotate(0, -01, 0, Space.Self);
            }

            if (Input.GetKeyDown (KeyCode.S)) {
                controllingObject = false;
                playerScript.enabled = true;
                ThirdPersonCameraController.instance.cameraPositionOverride = null;
            }

            if(Input.GetKeyDown (KeyCode.Space)) {
                towerBridgeMove.OpenTowerRamp();
                towerGateMove.LowerTowerGate();
            }
        }
    }

    void OnCollisionEnter (Collision collision) {
        //checking collision to control objects

        if(collision.gameObject.CompareTag ("Player")) {
            playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.enabled = false;
            controllingObject = true;
            if(cameraPositionOverride) {
                ThirdPersonCameraController.instance.cameraPositionOverride = cameraPositionOverride;
            }
         }
    }
}
