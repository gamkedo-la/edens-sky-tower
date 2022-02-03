using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMovement : MonoBehaviour
{
    public string keyNumber;
    public Transform positionUnreachable;
    public Transform positionFinal;
    private Vector3 positionReachable; // where it starts in the editor

    GameObject Player;

    void Awake() 
    {
       positionReachable = transform.position;
       SnapToPosition();
    }

    
    void SnapToPosition()
    {
         bool endKey = PlayerPrefs.GetInt("usedKey4", 0) == 1;
        if(endKey) {
             transform.position = positionFinal.position;
             return;
        }
         bool keyUsed = PlayerPrefs.GetInt("usedKey" + keyNumber, 0) == 1;
         if(keyUsed) {
             transform.position = positionReachable;
         } else {
             transform.position = positionUnreachable.position;
         }
    }

    // Update is called once per frame
    void Update()
    {
      SnapToPosition();
    }
}
