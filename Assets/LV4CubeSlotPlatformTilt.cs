using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV4CubeSlotPlatformTilt : MonoBehaviour
{

    public puzzleSceneCubePairing cubePairingScriptOne;
    public puzzleSceneCubePairing cubePairingScriptTwo;
    public puzzleSceneCubePairing cubePairingScriptThree;
    public puzzleSceneCubePairing cubePairingScriptFour;

    public TiltingPlatformMove seesawPlatformScript;

    public GameObject textBox;

    void Update()
    {
        if(cubePairingScriptOne.cubeOneSlot && cubePairingScriptTwo.cubeTwoSlot && cubePairingScriptThree.cubeThreeSlot && cubePairingScriptFour.cubeFourSlot)
        {
            Debug.Log("Platform to tilt");
            seesawPlatformScript.tiltPlatformBackwards();
            if(textBox != null)
            {
                textBox.SetActive(true);
            }
            
        }
    }
}
