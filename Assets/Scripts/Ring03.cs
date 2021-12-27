using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring03 : MonoBehaviour
{
    public Transform target;
    public float t;
    bool triggerRing = false;
    bool triggerRings;

    GameObject Player;

    void Awake() 
    {
        Player = GameObject.Find("PLAYER");
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Script started for: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        triggerRings = Player.GetComponent<P2Class>().TriggerRingsThree;

       
       // if (Input.GetKeyDown(KeyCode.Alpha2))
       if(triggerRings)
        {
            //Debug.Log("Space key was pressed.");
            triggerRing = true;
        }
        
        if(triggerRing) {
            Vector3 a = transform.position;
            Vector3 b = target.position;
            transform.position = Vector3.Lerp(a,b, t); 
        }
        
    }
}
