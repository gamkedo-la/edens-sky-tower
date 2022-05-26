using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerMonolithMovement : MonoBehaviour
{
    public towerMonolithMove towerMonolithMove;
    public LevelTowerSelection LevelTowerSelectionScript;

    void OnTriggerExit (Collider other) {
        if(towerMonolithMove.riseMonolith)
        {
            towerMonolithMove.RiseMonolith();
            LevelTowerSelectionScript.selectedLevel = false;
        }
    }
}

