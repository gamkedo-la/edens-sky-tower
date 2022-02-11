using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform ToShardOne;
    public Transform ToShardTwo;
    public Transform ToShardThree;
    public Transform LevBase1;
    public Transform LevBase2;
    public Transform LevBase3;
    public bool PlayerTransferShard1 = false;
    public bool PlayerTransferShard2 = false;
    public bool PlayerTransferShard3 = false;

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
            PlayerTransferShard1 = false;
        }

        if(PlayerTransferShard2) {
            gameObject.transform.position = ToShardTwo.position;
            PlayerTransferShard2 = false;
        }

        if(PlayerTransferShard3) {
            gameObject.transform.position = ToShardThree.position;
            PlayerTransferShard3 = false;
        }

        //debug
        if (Input.GetKeyDown(KeyCode.T)) {
            transform.position = TeleportDebugLocation.position;
            Debug.Log ("teleproting Debug");
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            PlayerPrefs.SetInt("usedKey1", 1);
            Debug.Log("used Key One");
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            PlayerPrefs.SetInt("usedKey2", 1);
            Debug.Log("used Key Two");
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            PlayerPrefs.SetInt("usedKey3", 1);
            Debug.Log("used Key Three");
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            PlayerPrefs.SetInt("usedKey4", 1);
            Debug.Log("used Key Four");
        }

        if(Input.GetKeyDown(KeyCode.Alpha5)) {
            PlayerPrefs.SetInt("usedKey1", 0);
            PlayerPrefs.SetInt("usedKey2", 0);
            PlayerPrefs.SetInt("usedKey3", 0);
            PlayerPrefs.SetInt("usedKey4", 0);
            Debug.Log("unuse all keys");
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
/*
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
*/
} // end of class
