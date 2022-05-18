using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBackwardTiltTrigger : MonoBehaviour
{
    public TiltingPlatformMove TiltingPlatformMove;

    void OnTriggerEnter (Collider other)
    {
        TiltingPlatformMove.tiltPlatformBackwards();
        
    }
}
