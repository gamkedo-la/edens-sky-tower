using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerBridgeMove : MonoBehaviour
{    
    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string TOWER_BRIDGE_EXTENDS = "shardBridgeExtends";
    private const string TOWER_BRIDGE_RECTRACTS = "shardBridgeRetracts";

    // Start is called before the first frame update
    void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        rendererComponent = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)){
            animatorComponent.Play(TOWER_BRIDGE_RECTRACTS);
        }
    }

    public void Disable() {
        rendererComponent.enabled = false;
    }

    public void Enable() {
        rendererComponent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("bridge to extend!");
            animatorComponent.Play(TOWER_BRIDGE_EXTENDS);
        }
    }
}
