using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerBridgeMove : MonoBehaviour
{    
    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string TOWER_BRIDGE_EXTENDS = "shardBridgeExtends";
    private const string TOWER_BRIDGE_RECTRACTS = "shardBridgeRetracts";
    private const string TOWER_RAMP_EXTENDS = "shardRampExtending";
    private const string TOWER_RAMP_RECTRACTS = "shardRampRetracting";

    // Start is called before the first frame update
    void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        rendererComponent = GetComponent<Renderer>();
    }

    public void OpenTowerBridge() {
        animatorComponent.Play(TOWER_BRIDGE_EXTENDS);
    }

    public void CloseTowerBridge() {
        animatorComponent.Play(TOWER_BRIDGE_RECTRACTS);
    }

    public void OpenTowerRamp() {
        animatorComponent.Play(TOWER_RAMP_EXTENDS);
    }

    public void CloseTowerRamp() {
        animatorComponent.Play(TOWER_RAMP_RECTRACTS);
    }

    public void Disable() {
        rendererComponent.enabled = false;
    }

    public void Enable() {
        rendererComponent.enabled = true;
    }

/*
    private void OnTriggerEnter(Collider other)
    {    
        animatorComponent.Play(TOWER_BRIDGE_EXTENDS);
    }
*/
}
