using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV4CubeKeyholeTrigger : MonoBehaviour
{
    public LoweringPlatformLV4Move LoweringPlatformLV4Move;
    public GameObject cubeKey;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Key Inserted!");
        cubeKey.SetActive(true);
        LoweringPlatformLV4Move.lowerPlatform();
    }
}
