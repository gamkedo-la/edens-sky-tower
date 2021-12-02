using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring01 : MonoBehaviour
{
    public Transform target;
    public float t;
    bool triggerRing = false;
    bool triggerRings;
    
    GameObject obj;

    void Awake() 
    {
        obj = GameObject.FindGameObjectWithTag ("TowerSegmentB");
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Script started for: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

        triggerRings = obj.GetComponent<TowerB>().segmentBEngaged;
        //if (Input.GetKeyDown(KeyCode.Alpha1))
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
