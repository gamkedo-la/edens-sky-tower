using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Class : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Script online for: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKey(KeyCode.UpArrow)) {
            transform.position += transform.forward * Time.deltaTime * 3.0f;
        }

        if(Input.GetKey(KeyCode.DownArrow)) {
            transform.position += transform.forward * Time.deltaTime * -3.0f;
        } */

        //transform.position += transform.forward * Time.deltaTime * 3.0f * Input.GetAxis("Vertical");
        Vector3 velWithGravity = rb.velocity;
        float saveYV = velWithGravity.y;
        velWithGravity = transform.forward * 6.0f * Input.GetAxis("Vertical");
        velWithGravity.y = saveYV;
        rb.velocity = velWithGravity;

        /*if(Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(Vector3.up, 30.0f * Time.deltaTime); 
        }

        if(Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(Vector3.up, -30.0f * Time.deltaTime); 
        } */

        transform.Rotate(Vector3.up, 30.0f * Time.deltaTime * Input.GetAxis("Horizontal")); 
        
        // player jump
        if(Input.GetKeyDown (KeyCode.Space)) {
            if (transform.position.y <=1.05f) {
                GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
            } 
        }

            } // end of Update
} // end of class
