using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGameScript : MonoBehaviour
{
    public towerMonolithMove towerMonolithMove;
    public ThirdPersonCameraController ThirdPersonCameraController;
    public Transform cameraOverrideEndScenePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter ()
    {

        if (cameraOverrideEndScenePos)
        {
            ThirdPersonCameraController.instance.cameraPositionOverride = cameraOverrideEndScenePos;
        }

        Debug.Log("game ends");
        towerMonolithMove.EndSceneMonolithLower();
    }
}
