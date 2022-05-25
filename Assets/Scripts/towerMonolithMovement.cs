using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerMonolithMovement : MonoBehaviour
{
    public towerMonolithMove towerMonolithMove;


    void OnTriggerExit (Collider other) {
        if(towerMonolithMove.riseMonolith)
        {
            towerMonolithMove.RiseMonolith();
        }
    }
}

