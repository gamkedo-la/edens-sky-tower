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

    public GameObject particleSystem;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == cubeName)
        {
            if (cubeName == "CubeOne")
            {
                cubeOneSlot = true;
                if (particleSystem != null) {
                    particleSystem.SetActive(false);
                }
            }
            if(cubeName == "CubeTwo")
            {
                cubeTwoSlot = true;
                if (particleSystem != null)
                {
                    particleSystem.SetActive(false);
                }
            }
            if(cubeName == "CubeThree")
            {
                cubeThreeSlot = true;
                if (particleSystem != null)
                {
                    particleSystem.SetActive(false);
                }
            }
            if(cubeName == "CubeFour")
            {
                cubeFourSlot = true;
                if (particleSystem != null)
                {
                    particleSystem.SetActive(false);
                }
            }
            //Debug.Log(cubeName + " One Inserted!");
            //Debug.Log(cubeOneSlot);
        }
    }
}
