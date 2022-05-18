using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformForwardTiltTrigger : MonoBehaviour
{
    public TiltingPlatformMove TiltingPlatformMove;

    void OnTriggerEnter (Collider other)
    {
        TiltingPlatformMove.tiltPlatformForward();
        
    }
}
