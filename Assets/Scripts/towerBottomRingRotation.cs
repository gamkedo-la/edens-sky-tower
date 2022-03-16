using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerBottomRingRotation : MonoBehaviour
{

    public GameObject RotatingObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatingObject.transform.Rotate(0, 0.005f, 0, Space.Self);
    }
}
