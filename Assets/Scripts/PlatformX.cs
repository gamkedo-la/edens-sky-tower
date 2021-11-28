using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformX : MonoBehaviour
{
    public Transform target;
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Script started for: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
     Vector3 a = transform.position;
     Vector3 b = target.position;
     transform.position = Vector3.Lerp(a,b, t);   
    }
}
