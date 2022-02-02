﻿using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform ToShardOne;
    public Transform ToShardTwo;
    public Transform ToShardThree;
    public Transform PlayerFallRespawnPoint;
    public Transform LevBase1;
    public Transform LevBase2;
    public Transform LevBase3;
    public bool PlayerTransferShard1 = false;
    public bool PlayerTransferShard2 = false;
    public bool PlayerTransferShard3 = false;

    public bool TriggerRingsOne = false;
    public bool TriggerRingsTwo = false;
    public bool TriggerRingsThree = false;
    public LayerMask jumpFrom;
    public Transform TeleportDebugLocation;
    public Transform TiltGlideModel;


    [Range(1.0f, 4.0f)]
    public float runningSpeedMultiplier = 2.0f;

    private Rigidbody rb;
    private bool holdingGlide = false;

    private bool isRunning = false;
   
    void Start()
    {
        jumpFrom = ~jumpFrom;
        rb = GetComponent<Rigidbody>();
        TiltGlideModel.localRotation = Quaternion.identity;
    }

    void ShowGlider(bool turnOn) {
        if(holdingGlide != turnOn) {
            holdingGlide = turnOn;
            if(holdingGlide) {
                TiltGlideModel.localRotation = Quaternion.AngleAxis(50.0f, Vector3.right);
            }else{
                TiltGlideModel.localRotation = Quaternion.identity;
            }
        }
    }

    void Update()
    {
        Vector3 velWithGravity = rb.velocity;
        float saveYV = velWithGravity.y;
        velWithGravity = transform.forward * 10.0f * Input.GetAxis("Vertical");

        checkIfChangingToRunningOrWalkingMode();
        if (isRunning && !holdingGlide) velWithGravity *= runningSpeedMultiplier;

        if(holdingGlide) {
            velWithGravity.y = -3.5f; 
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
        }

        if(PlayerTransferShard3) {
            gameObject.transform.position = ToShardThree.position;
            TriggerRingsThree = true;
            PlayerTransferShard3 = false;
        }

        //player respawns when falling
        if(transform.position.y < -5.0f) {
            gameObject.transform.position = PlayerFallRespawnPoint.position;
        }

        //debug
        if (Input.GetKeyDown(KeyCode.T)) {
            transform.position = TeleportDebugLocation.position;
            Debug.Log ("teleproting Debug");
        }

    } // end of Update

    private void checkIfChangingToRunningOrWalkingMode()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
    }

    void OnCollisionEnter (Collision collision) {

         if (collision.gameObject.CompareTag ("LevitatingBase1")) {
             transform.position = LevBase1.position;
             Debug.Log ("LevBase 1 collision");
         }

         if (collision.gameObject.CompareTag ("LevitatingBase2")) {
             Debug.Log ("LevBase 2 collision");
             transform.position = LevBase2.position;
         }
         
         if (collision.gameObject.CompareTag ("LevitatingBase3")) {
             transform.position = LevBase3.position;
         }
    }
} // end of class
