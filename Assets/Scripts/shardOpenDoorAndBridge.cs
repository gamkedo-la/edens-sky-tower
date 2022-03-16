using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shardOpenDoorAndBridge : MonoBehaviour
{
    public towerBridgeMove towerBridgeMove;
    public towerDoorMove towerDoorMove;


    void OnTriggerEnter (Collider other) {
        towerBridgeMove.OpenTowerBridge();
        towerDoorMove.OpenTowerDoor();

    }
}

