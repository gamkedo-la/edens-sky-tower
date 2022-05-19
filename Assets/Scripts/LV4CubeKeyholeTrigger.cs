using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV4CubeKeyholeTrigger : MonoBehaviour
{
    public LoweringPlatformLV4Move LoweringPlatformLV4Move;
    public GameObject cubeKey;
    public GameObject textBox;

    public Player playerScript; // referencing Player script

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding");
        if(other.tag == "CubeKey")
        {
            Debug.Log("Key Inserted!");
            playerScript.carryingLV4CubeKey = false;
            cubeKey.SetActive(true);
            LoweringPlatformLV4Move.lowerPlatform();
            textBox.SetActive(true);
        }
        
    }
}
