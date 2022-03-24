using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerMonolithMovement : MonoBehaviour
{
    public towerMonolithMove towerMonolithMove;


    void OnTriggerEnter (Collider other) {
        towerMonolithMove.LowerMonolith();
    }
}

