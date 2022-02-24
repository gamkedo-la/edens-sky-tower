using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMovement : MonoBehaviour
{
    public string keyNumber;
    public Transform positionUnreachable;
    public Transform positionFinal;
    [SerializeField]private float smoothRate = 0.002f;
    private Vector3 positionReachable; // where it starts in the editor
    private Vector3 destination;

    GameObject Player;

    void Awake() 
    {
       positionReachable = transform.position;
        destination = transform.position;
       SnapToPosition();
       transform.position = destination; // player shouldn't see transition at start
    }

    
    void SnapToPosition()
    {
         bool endKey = PlayerPrefs.GetInt("usedKey4", 0) == 1;
        if(endKey) {
             //transform.position = positionFinal.position;
            destination = positionFinal.position;
             return;
        }
         bool keyUsed = PlayerPrefs.GetInt("usedKey" + keyNumber, 0) == 1;
         if(keyUsed) {
             //transform.position = positionReachable;
            destination = positionReachable;
        } else {
             //transform.position = positionUnreachable.position;
            destination = positionUnreachable.position;
        }
    }

    void FixedUpdate() // not using update since move destination uses lerp
    {
      SnapToPosition();
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        transform.position = Vector3.Lerp(transform.position, destination, smoothRate);
    }
}
