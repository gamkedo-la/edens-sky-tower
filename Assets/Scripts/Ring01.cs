using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring01 : MonoBehaviour
{
    public Transform target;
    public float t;
    bool triggerRings;
    
    GameObject Player;

    void Awake() 
    {
        Player = GameObject.Find("PLAYER");
    }

    void Update()
    {
        triggerRings = Player.GetComponent<P2Class>().TriggerRingsOne;

        if(triggerRings)
        {
            Vector3 a = transform.position;
            Vector3 b = target.position;
            transform.position = Vector3.Lerp(a,b, t); 
        }
        
    }
}
