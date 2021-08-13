using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControls : MonoBehaviour
{
    public float speed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        transform.position += Vector3.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
    }
}
