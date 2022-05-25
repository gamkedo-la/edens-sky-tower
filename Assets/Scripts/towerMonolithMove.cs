using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerMonolithMove : MonoBehaviour
{    
    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string TOWER_MONOLITH_RISING = "MonolithRising";
    private const string TOWER_MONOLITH_LOWERING = "MonolithLowering";

    public bool riseMonolith = false;

    // Start is called before the first frame update
    void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        rendererComponent = GetComponent<Renderer>();
    }

   public void LowerMonolith() {
        riseMonolith = true;
        animatorComponent.Play(TOWER_MONOLITH_LOWERING);
    }

    public void RiseMonolith() {
        riseMonolith = false;
        animatorComponent.Play(TOWER_MONOLITH_RISING);
    }

    public void Disable() {
        rendererComponent.enabled = false;
    }

    public void Enable() {
        rendererComponent.enabled = true;
    }
}
