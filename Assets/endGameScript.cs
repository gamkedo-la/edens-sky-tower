using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGameScript : MonoBehaviour
{
    public towerMonolithMove towerMonolithMove;
    public ThirdPersonCameraController ThirdPersonCameraController;
    public Transform cameraOverrideEndScenePos;
    public EndSceneAnimationsScripts EndSceneAnimationsScripts;
    public Transform endScenePos;
    public Player Player;
    bool finalPlayerPos = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(finalPlayerPos)
        {
            Player.transform.position = endScenePos.position;
        }
    }

    void OnTriggerEnter ()
    {

        if (cameraOverrideEndScenePos)
        {
            finalPlayerPos = true;
            //Player.transform.position = endScenePos.position;
            ThirdPersonCameraController.instance.cameraPositionOverride = cameraOverrideEndScenePos;
            EndSceneAnimationsScripts.EndSceneFadeInBackg();
            EndSceneAnimationsScripts.EndSceneFadeInText();
        }

        Debug.Log("game ends");
        towerMonolithMove.EndSceneMonolithLower();
        
    }
}
