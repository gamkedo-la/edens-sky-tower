using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV4CubeSlotPlatformTilt : MonoBehaviour
{

    public puzzleSceneCubePairing cubePairingScriptOne;
    public puzzleSceneCubePairing cubePairingScriptTwo;
    public puzzleSceneCubePairing cubePairingScriptThree;
    public puzzleSceneCubePairing cubePairingScriptFour;

    void Update()
    {
        if(cubePairingScriptOne.cubeOneSlot && cubePairingScriptTwo.cubeTwoSlot && cubePairingScriptThree.cubeThreeSlot && cubePairingScriptFour.cubeFourSlot)
        {
            Debug.Log("Platform to tilt");
        }
    }
}
