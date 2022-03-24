using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerMonolithMove : MonoBehaviour
{    
    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string TOWER_MONOLITH_RISING = "MonolithRising";
    private const string TOWER_MONOLITH_LOWERING = "MonolithLowering";

    // Start is called before the first frame update
    void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        rendererComponent = GetComponent<Renderer>();
    }

   public void LowerMonolith() {
       Debug.Log("Monolith lowering");
        animatorComponent.Play(TOWER_MONOLITH_LOWERING);
    }

    public void RiseMonolith() {
       Debug.Log("Monolith Rising");
        animatorComponent.Play(TOWER_MONOLITH_RISING);
    }

    public void Disable() {
        rendererComponent.enabled = false;
    }

    public void Enable() {
        rendererComponent.enabled = true;
    }
}
