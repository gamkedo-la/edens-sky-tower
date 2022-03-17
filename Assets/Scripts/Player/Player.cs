using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Transform ToShardOne;
    public Transform ToShardTwo;
    public Transform ToShardThree;
    public bool PlayerTransferShard1 = false;
    public bool PlayerTransferShard2 = false;
    public bool PlayerTransferShard3 = false;
    public GameObject CarryingKey;

    public LayerMask jumpFrom;
    public Transform TeleportDebugLocation;
    public Transform TiltGlideModel;


    [Range(1.0f, 4.0f)]
    public float runningSpeedMultiplier = 2.0f;

    private Rigidbody rb;
    private bool holdingGlide = false;

    private bool isRunning = false;
    public bool isAffectedByWind = false;

    void Awake() {
        instance = this;
    }

    void Start()
    {
        jumpFrom = ~jumpFrom;
        rb = GetComponent<Rigidbody>();
        TiltGlideModel.localRotation = Quaternion.identity;
        bool isHeld = PlayerPrefs.GetInt("holdKey" + 1, 0) == 1;
        CarryingKey.SetActive(isHeld);
    }

    public void GetKey (string whichKey) {
        PlayerPrefs.SetInt("holdKey" + whichKey, 1);
        CarryingKey.SetActive(true);
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
        if (isAffectedByWind) // if affected by wind, don't allow the player to move
        {
            return; 
        }
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
            if(rb.velocity.y < -15.0f) { // temporary change, raycast detects wind trigger otherwise (WIP)
                rb.velocity = Vector3.zero;
            }
            transform.SetParent (rhInfo.collider.transform);
            if(Input.GetKeyDown (KeyCode.Space)) {
                rb.AddForce (Vector3.up * 350);
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
            PlayerPrefs.SetInt("holdKey1", 0);
            PlayerPrefs.SetInt("holdKey2", 0);
            PlayerPrefs.SetInt("holdKey3", 0);
            PlayerPrefs.SetInt("holdKey4", 0);
            Debug.Log("unuse all keys");
        }
    
        if(Input.GetKeyDown(KeyCode.Alpha6)) {
            PlayerPrefs.DeleteAll();
            Debug.Log ("forgetting all player pref - reloding scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    void InsertKey(int keyNumber, GameObject consoleKeyGO) {
        Debug.Log("Key Inserted!");
        CarryingKey.SetActive(false);
        ConsoleKeyManager CKMScript = consoleKeyGO.GetComponent<ConsoleKeyManager>();
        if(CKMScript) {
            CKMScript.UseKey();
        } else {
            Debug.LogWarning("console is missing a ConsoleKeyManager");
        }

    }

    void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.CompareTag ("KeyHoleA")) {
            bool isHeld1 = PlayerPrefs.GetInt("holdKey" + 1, 0) == 1;
            if(isHeld1) {
               InsertKey(1, collision.gameObject);
            } 
        }

        if (collision.gameObject.CompareTag ("KeyHoleB")) {
            bool isHeld2 = PlayerPrefs.GetInt("holdKey" + 2, 0) == 1;
            if(isHeld2) {
               InsertKey(2, collision.gameObject);
            } 
        }

        if (collision.gameObject.CompareTag ("KeyHoleC")) {
            bool isHeld3 = PlayerPrefs.GetInt("holdKey" + 3, 0) == 1;
            if(isHeld3) {
               InsertKey(3, collision.gameObject);
            } 
        }

        if (collision.gameObject.CompareTag ("KeyHoleD")) {
            bool isHeld4 = PlayerPrefs.GetInt("holdKey" + 4, 0) == 1;
            if(isHeld4) {
               InsertKey(4, collision.gameObject);
            } 
        }
         
    }

} // end of class
