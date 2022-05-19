using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerPlatformLV4Trigger : MonoBehaviour
{
    public LoweringPlatformLV4Move LoweringPlatformLV4Move;
    void OnTriggerEnter (Collider other)
    {
        LoweringPlatformLV4Move.lowerPlatform();
    }

}
