using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerD : MonoBehaviour
{
    public bool segmentDEngaged = false;
    public bool keyHoleCActivated = false;
    bool startDescent = false;
    bool startAscent = false;
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

        Debug.Log (keyHoleCActivated);


        //if(Input.GetKeyDown(KeyCode.Keypad3)) {

        if(keyHoleCActivated) {
            Debug.Log(gameObject.name + " Tower Segment to Descend!");
            startDescent = true; 
            startAscent = false; 
        }

        if(!segmentDEngaged && startDescent) {
            transform.position += transform.up * Time.deltaTime * -3.0f;
            }

        if(Input.GetKeyDown(KeyCode.Keypad0)) {
            Debug.Log("Tower to original Pos: " + gameObject.name);
            startAscent = true; 
            segmentDEngaged = false;
            startDescent = false;
        }

        if(startAscent && !segmentDEngaged && transform.position.y < originalPos) {
            transform.position += transform.up * Time.deltaTime * 3.0f;
        }
        
    }

    void OnCollisionEnter (Collision collision) {
        // Debug.Log(gameObject.name + " Tower Segment Engaged");
        segmentDEngaged = true;
        startDescent = false;
    }
}