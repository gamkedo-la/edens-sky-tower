using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void Start () {
        if(Camera.main == null) {
            Debug.Log ("no main camera found terminating billboard");
            this.enabled = false;
        }
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(Vector3.up,180.0f);
    }
}
