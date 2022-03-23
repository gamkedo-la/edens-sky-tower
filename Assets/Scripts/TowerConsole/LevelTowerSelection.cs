using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTowerSelection : MonoBehaviour
{
    private bool triggering;

    public GameObject toolTipText;
  
    void Start()
    {
        toolTipText.SetActive(false);
    }

    void Update()
    {
        if(triggering)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("Console has been activated.");
            }
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            toolTipText.SetActive(true);
            triggering = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            toolTipText.SetActive(false);
            triggering = false;
        }
    }

}