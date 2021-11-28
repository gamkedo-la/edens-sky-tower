using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformX : MonoBehaviour
{
    float originalPosY;
    float originalPosX;
    float originalPosZ;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script started for: " + gameObject.name);
        originalPosY = transform.position.y;
        originalPosX = transform.position.x;
        originalPosZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * -3.0f;
    }
}
