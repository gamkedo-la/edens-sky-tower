using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Class : MonoBehaviour
{
    Rigidbody rb;
    public Transform ToShardOne;
    public Transform ToShardTwo;
    public Transform ToShardThree;
    public bool PlayerTransferShard1 = false;
    public bool PlayerTransferShard2 = false;
    public bool PlayerTransferShard3 = false;

    public bool TriggerRingsOne = false;
    public bool TriggerRingsTwo = false;
    public bool TriggerRingsThree = false;
    public LayerMask jumpFrom;
   
    void Start()
    {
        jumpFrom = ~jumpFrom;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 velWithGravity = rb.velocity;
        float saveYV = velWithGravity.y;
        velWithGravity = transform.forward * 6.0f * Input.GetAxis("Vertical");
        velWithGravity.y = saveYV;
        rb.velocity = velWithGravity;

        transform.Rotate(Vector3.up, 30.0f * Time.deltaTime * Input.GetAxis("Horizontal")); 
        
        // player jump
        
        RaycastHit rhInfo;
        if (Physics.Raycast (transform.position, Vector3.down, out rhInfo, 1.5f, jumpFrom)) {
            Debug.Log (rb.velocity.y);
            if(rb.velocity.y < 0.0f) {
                rb.velocity = Vector3.zero;
            }
            transform.SetParent (rhInfo.collider.transform);
            if(Input.GetKeyDown (KeyCode.Space)) {
                GetComponent<Rigidbody> ().AddForce (Vector3.up * 500);
            } 
        }

        if(PlayerTransferShard1) {
            gameObject.transform.position = ToShardOne.position;
            TriggerRingsOne = true;
            PlayerTransferShard1 = false;
        }

        if(PlayerTransferShard2) {
            gameObject.transform.position = ToShardTwo.position;
            TriggerRingsTwo = true;
            PlayerTransferShard2 = false;
            Debug.Log ("Shard2, once?");
        }

        if(PlayerTransferShard3) {
            gameObject.transform.position = ToShardThree.position;
            TriggerRingsThree = true;
            PlayerTransferShard3 = false;
        }

            } // end of Update
} // end of class
