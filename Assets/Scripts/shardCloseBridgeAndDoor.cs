using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardCloseBridgeAndDoor : MonoBehaviour
{
    public towerBridgeMove towerBridgeMove;
    public towerDoorMove towerDoorMove;
    public ElevatorPlatform elevPlatformScript;


    void OnTriggerEnter (Collider other) {
        towerBridgeMove.CloseTowerBridge();
        towerDoorMove.CloseTowerDoor();
        elevPlatformScript.resetElevPlatform = true;

    }
}
