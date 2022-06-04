using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGameScript : MonoBehaviour
{
    public towerMonolithMove towerMonolithMove;
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
        Debug.Log("game ends");
        towerMonolithMove.EndSceneMonolithLower();
    }
}
