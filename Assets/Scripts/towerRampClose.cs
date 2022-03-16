using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerRampClose : MonoBehaviour
{

public towerBridgeMove towerBridgeMove;

void OnTriggerEnter (Collider other) {
        towerBridgeMove.CloseTowerRamp();

    }

}
