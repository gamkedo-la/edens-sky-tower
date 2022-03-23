using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTowerSelection : MonoBehaviour
{
    private bool triggering;

    public Text toolTipText;
  
    void Start()
    {
        toolTipText.gameObject.SetActive(false);
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
            toolTipText.text = "Which Floor?\n";

            bool noKeys = true;

            if(PlayerPrefs.GetInt("usedKey1", 0) == 1)
            {
                toolTipText.text += "U: Floor One\n";
                noKeys = false;
            }

            if (PlayerPrefs.GetInt("usedKey2", 0) == 1)
            {
                toolTipText.text += "I: Floor Two\n";
                noKeys = false;
            }

            if (PlayerPrefs.GetInt("usedKey3", 0) == 1)
            {
                toolTipText.text += "O: Floor Three\n";
                noKeys = false;
            }

            if (noKeys)
            {
                toolTipText.text += "[Search for Keys]";
            }

            toolTipText.gameObject.SetActive(true);
            triggering = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            toolTipText.gameObject.SetActive(false);
            triggering = false;
        }
    }

}