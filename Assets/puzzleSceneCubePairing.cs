using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleSceneCubePairing : MonoBehaviour
{
    public string cubeName;

    public bool cubeOneSlot = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == cubeName)
        {
            if (cubeName == "CubeOne")
            {
                cubeOneSlot = true;
            }
            Debug.Log(cubeName + " One Inserted!");
            Debug.Log(cubeOneSlot);
        }

        /*

        if (other.gameObject.name == "CubeTwo")
        {
            Debug.Log("Cube Two Inserted!");
        }

        if (other.gameObject.name == "CubeThree")
        {
            Debug.Log("Cube Three Inserted!");
        }

        if (other.gameObject.name == "CubeFour")
        {
            Debug.Log("Cube Four Inserted!");
        } */
    }
}
