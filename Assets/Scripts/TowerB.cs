using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerB : MonoBehaviour
{
    public bool segmentBEngaged = false;
    bool startDescent = false;
    bool startAscent = false;
    public bool keyHoleAActivated = false;
    float originalPos;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Script started for: " + gameObject.name);
        originalPos = transform.position.y;
        // Debug.Log(transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log (keyHoleAActivated);

     //  if(Input.GetKeyDown(KeyCode.Keypad1)) {

        if(keyHoleAActivated) {
            Debug.Log(gameObject.name + " Tower Segment to Descend!");
            startDescent = true; 
            startAscent = false; 
        }

        if(!segmentBEngaged && startDescent) {
            transform.position += transform.up * Time.deltaTime * -3.0f;
            }

        if(Input.GetKeyDown(KeyCode.Keypad0)) {
            Debug.Log("Tower to original Pos: " + gameObject.name);
            startAscent = true; 
            segmentBEngaged = false;
            startDescent = false;
        }

        if(startAscent && !segmentBEngaged && transform.position.y < originalPos) {
            transform.position += transform.up * Time.deltaTime * 3.0f;
        }
        
    }

    void OnCollisionEnter (Collision collision) {
        // Debug.Log(gameObject.name + " Tower Segment Engaged");
        segmentBEngaged = true;
        startDescent = false;
    }
}
