using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserMirrorMovement : MonoBehaviour
{
    private bool triggering;

    public Text toolTipText;

    public Vector3 point; //position of the point you want to rotate around

    void Start()
    {
        toolTipText.gameObject.SetActive(false);
    }

    void Update()
    {
        if(triggering)
        {
            if (Input.GetKey(KeyCode.U))
            {
                print("Console has been activated.");
                transform.Rotate(/*point,*/ Vector3.forward, 20 * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.I))
            {
                print("Console has been activated.");
                transform.Rotate(/*point,*/ -Vector3.forward, 20 * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            toolTipText.text = "Rotate Left = U\n";
            toolTipText.text += "Rotate Right = I\n";

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