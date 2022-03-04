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

   public void CloseTowerDoor() {
       Debug.Log("Door to close!");
        animatorComponent.Play(TOWER_DOOR_CLOSE);
    }

    public void OpenTowerDoor() {
       Debug.Log("Door to close!");
        animatorComponent.Play(TOWER_DOOR_OPEN);
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
        Debug.Log("Door to open!");
        animatorComponent.Play(TOWER_DOOR_OPEN);

    }
*/
}
