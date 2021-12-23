using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Class : MonoBehaviour
{
    Rigidbody rb;
    public Transform ToShardOne;
    public bool PlayerTransferShard1 = false;
   
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Script online for: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward * Time.deltaTime * 3.0f * Input.GetAxis("Vertical");
        Vector3 velWithGravity = rb.velocity;
        float saveYV = velWithGravity.y;
        velWithGravity = transform.forward * 6.0f * Input.GetAxis("Vertical");
        velWithGravity.y = saveYV;
        rb.velocity = velWithGravity;

        transform.Rotate(Vector3.up, 30.0f * Time.deltaTime * Input.GetAxis("Horizontal")); 
        
        // player jump
        if(Input.GetKeyDown (KeyCode.Space)) {
            if (transform.position.y <=1.05f) {
                GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
            } 
        }

        if(PlayerTransferShard1) {
            gameObject.transform.position = ToShardOne.position;
            PlayerTransferShard1 = false;
        }

            } // end of Update
} // end of class
