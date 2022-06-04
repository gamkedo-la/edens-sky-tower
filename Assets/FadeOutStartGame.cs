using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutStartGame : MonoBehaviour
{
    public EndSceneAnimationsScripts EndSceneAnimationsScripts;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter (Collider other)
    {
        if(other.tag == "Player")
        {
            EndSceneAnimationsScripts.StartSceneFadeOutBackg();
            Debug.Log("fade out white");
        }
    }
}
