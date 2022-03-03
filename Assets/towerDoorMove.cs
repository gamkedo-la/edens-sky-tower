using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerDoorMove : MonoBehaviour
{
    private Animator animatorComponent;
    private Renderer rendererComponent;

    private const string TOWER_DOOR_CLOSE = "shardDoorClosing";
    private const string TOWER_DOOR_OPEN = "shardDoorOpening";

    // Start is called before the first frame update
    void Awake()
    {
        animatorComponent = GetComponent<Animator>();
        rendererComponent = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            animatorComponent.Play(TOWER_DOOR_OPEN);
        }

        if(Input.GetKeyDown(KeyCode.I)){
            animatorComponent.Play(TOWER_DOOR_CLOSE);
        }
    }

    public void Disable() {
        rendererComponent.enabled = false;
    }

    public void Enable() {
        rendererComponent.enabled = true;
    }
}
