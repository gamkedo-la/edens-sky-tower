using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleSceneCubePairing : MonoBehaviour
{
    public string cubeName;

    public bool cubeOneSlot = false;
    public bool cubeTwoSlot = false;
    public bool cubeThreeSlot = false;
    public bool cubeFourSlot = false;    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == cubeName)
        {
            if (cubeName == "CubeOne")
            {
                cubeOneSlot = true;
            }
            if(cubeName == "CubeTwo")
            {
                cubeTwoSlot = true;
            }
            if(cubeName == "CubeThree")
            {
                cubeThreeSlot = true;
            }
            if(cubeName == "CubeFour")
            {
                cubeFourSlot = true;
            }
            //Debug.Log(cubeName + " One Inserted!");
            //Debug.Log(cubeOneSlot);
        }
    }
}
