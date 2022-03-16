using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerExit : MonoBehaviour
{

public towerBridgeMove towerBridgeMove;
public towerGateMove towerGateMove;

void OnTriggerEnter (Collider other) {
        towerBridgeMove.CloseTowerRamp();
        towerGateMove.RiseTowerGate();

    }

}
