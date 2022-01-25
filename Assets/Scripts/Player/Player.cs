using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    public Transform TeleportDebugLocation;
    public GameObject GliderModel;

    private Rigidbody rb;
    private bool holdingGlide = false;
   
    void Start()
    {
        jumpFrom = ~jumpFrom;
        rb = GetComponent<Rigidbody>();
        GliderModel.SetActive(false);
    }

    void ShowGlider(bool turnOn) {
        if(holdingGlide != turnOn) {
            holdingGlide = turnOn;
            GliderModel.SetActive(holdingGlide);
        }
    }

    void Update()
    {
        Vector3 velWithGravity = rb.velocity;
        float saveYV = velWithGravity.y;
        velWithGravity = transform.forward * 10.0f * Input.GetAxis("Vertical");
        if(holdingGlide) {
            velWithGravity.y = -4.5f; 
        } else {
            velWithGravity.y = saveYV; 
        }
        rb.velocity = velWithGravity;

        transform.Rotate(Vector3.up, 60.0f * Time.deltaTime * Input.GetAxis("Horizontal")); 
        
        // player jump
        
        RaycastHit rhInfo;
        //ground beneath us?
        if (Physics.Raycast (transform.position, Vector3.down, out rhInfo, 1.5f, jumpFrom)) {
            if(rb.velocity.y < 0.0f) {
                rb.velocity = Vector3.zero;
            }
            transform.SetParent (rhInfo.collider.transform);
            if(Input.GetKeyDown (KeyCode.Space)) {
                rb.AddForce (Vector3.up * 500);
            } 

            ShowGlider(false);
        } else {
            if(Input.GetKeyDown (KeyCode.Space)) {
                ShowGlider(true);
            }
            if(Input.GetKeyUp(KeyCode.Space)) {
                ShowGlider(false);
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

        //debug
        if (Input.GetKeyDown(KeyCode.T)) {
            transform.position = TeleportDebugLocation.position;
            Debug.Log ("teleproting Debug");
        }

    } // end of Update
} // end of class
