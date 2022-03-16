using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerGateMove : MonoBehaviour
{

    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string TOWER_GATE_LOWERS = "shardGateLowering";
    private const string TOWER_GATE_RISE = "shardGateRising";

        void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        rendererComponent = GetComponent<Renderer>();
    }

    public void LowerTowerGate() {
        animatorComponent.Play(TOWER_GATE_LOWERS);
    }

    public void RiseTowerGate() {
        animatorComponent.Play(TOWER_GATE_RISE);
    }


}
