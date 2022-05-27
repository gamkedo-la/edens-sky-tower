using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Transform ToShardOne;
    public Transform ToShardTwo;
    public Transform ToShardThree;
    public bool carryingLV4CubeKey = false;
    public GameObject LV4CubeKey;
    public GameObject[] CarryingKey;
    public GameObject InteractTip = null;
    public LevelTowerSelection LevelTowerSelection;
    public towerMonolithMove towerMonolithMove;
    public TowerElevator TowerElevatorScript;
    public LevelTowerSelection LevelTowerSelectionScript;
    private bool pauseMenuON = false;
    public GameObject PauseMenu;
 

    public LayerMask jumpFrom;
    public Transform TeleportDebugLocation;
    public Transform RespawnLocation;
    public bool playerFell = false;
    public Transform TiltGlideModel;

    private NESWPushable blockInFrontOfUs;
    private bool grabbingBlock = false;

    [Range(0, 20)]
    public float baseMovementSpeed = 5.0f;
    [Range(1.0f, 4.0f)]
    public float runningSpeedMultiplier = 1.5f;
    private float maximumForwardSpeed;

    private Rigidbody rb;
    public Animator animator;
    private bool holdingGlide = false;

    private bool isRunning = false;
    public bool isAffectedByWind = false;
    private bool isGrounded = false;
    public float jumpDelay = 0.125f;

    public Collider physicsCollider;

    public InventoryUI sotryTabletUICount; // for debug function

    void Awake() {
        instance = this;
    }

    void Start()
    {
        jumpFrom = ~jumpFrom;
        rb = GetComponent<Rigidbody>();
        TiltGlideModel.localRotation = Quaternion.identity;
        for (int i = 0; i < CarryingKey.Length; i++) {
            bool isHeld = PlayerPrefs.GetInt("holdKey" + (i+1), 0) == 1;
            CarryingKey[i].SetActive(isHeld);
        }
        
        animator = GetComponentInChildren<Animator>();
        CalculateMaximumForwardSpeed();

        InteractTip.SetActive(false);
    }

    public void GetKey (string whichKey) {
        PlayerPrefs.SetInt("holdKey" + whichKey, 1);
        for (int i = 0; i < CarryingKey.Length; i++)
        {
            bool isHeld = PlayerPrefs.GetInt("holdKey" + (i+1), 0) == 1;
            CarryingKey[i].SetActive(isHeld);
        }
    }

    void ShowGlider(bool turnOn) {
        if(holdingGlide != turnOn) {
            holdingGlide = turnOn;
            if(holdingGlide) {
                // TiltGlideModel.localRotation = Quaternion.AngleAxis(50.0f, Vector3.right);
                TiltGlideModel.gameObject.SetActive(true);
            }else{
                TiltGlideModel.gameObject.SetActive(false);
                // TiltGlideModel.localRotation = Quaternion.identity;
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
        float playerForwardBackwardSpeed = baseMovementSpeed *Input.GetAxis("Vertical");
        velWithGravity = transform.forward * playerForwardBackwardSpeed;

        checkIfChangingToRunningOrWalkingMode();
        if (isRunning && !holdingGlide)
        {
            playerForwardBackwardSpeed *= runningSpeedMultiplier;
            velWithGravity *= runningSpeedMultiplier;
        }

        if(holdingGlide) {
            velWithGravity.y = -1.0f; //gliding falling speed
        } else {
            velWithGravity.y = saveYV; 
        }
        rb.velocity = velWithGravity;
        animator.SetFloat("Speed", playerForwardBackwardSpeed / maximumForwardSpeed);
        animator.SetBool("Moving", Mathf.Abs(playerForwardBackwardSpeed) > 0);

        var rotationAngle = 60.0f * Input.GetAxis("Horizontal");
        animator.SetFloat("TurnAngle", rotationAngle);
        transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime); 
        
        if(InteractTip.activeSelf != (blockInFrontOfUs != null))
        {
            InteractTip.SetActive(blockInFrontOfUs != null);
        }
        
        RaycastHit rhInfo;
        //ground beneath us?
        var testDistance = physicsCollider.bounds.size.y * 0.51f;
        if (Physics.Raycast(physicsCollider.bounds.center, Vector3.down, out rhInfo, testDistance, jumpFrom))
        {
            
            if (!isGrounded)
            {
                isGrounded = true;
                animator.SetBool("Gliding", false);
                animator.ResetTrigger("Fall");
                animator.SetTrigger("Land");
            }

            if (rb.velocity.y < -15.0f)
            {
                // temporary change, raycast detects wind trigger otherwise (WIP)
                rb.velocity = Vector3.zero;
            }

            transform.SetParent(rhInfo.collider.transform);

            ShowGlider(false);
            // checking for block in front of us to pull/push
            if (Physics.Raycast(physicsCollider.bounds.center, transform.forward, out rhInfo, 3.5f, jumpFrom))
            {
                NESWPushable blockHere = rhInfo.collider.GetComponent<NESWPushable>();
                blockInFrontOfUs = blockHere;
            }
            else if (grabbingBlock == false && blockInFrontOfUs)
            {
                blockInFrontOfUs.ReleaseForgetDir();
                blockInFrontOfUs = null;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (blockInFrontOfUs)
                {
                    grabbingBlock = !grabbingBlock;
                    if (grabbingBlock == false)
                    {
                        if (blockInFrontOfUs)
                        {
                            blockInFrontOfUs.ReleaseForgetDir();
                            blockInFrontOfUs = null;
                        }
                    }
                }
                else
                {
                    //jump if no block in front of us
                    animator.SetTrigger("Jump");
                    StartCoroutine(AnimCallbackJump());
                }
            }

            if (grabbingBlock && blockInFrontOfUs)
            {
                if (blockInFrontOfUs.PushOrPull(transform))
                {
                    blockInFrontOfUs = null;
                }
            }
        } else {
            // if we've jumped off a platform entirely, unparent from it
            testDistance = 20f;
            if (!Physics.Raycast(physicsCollider.bounds.center, Vector3.down, out rhInfo, testDistance, jumpFrom))
            {
                transform.SetParent(null);
            }

            isGrounded = false;
            animator.SetTrigger("Fall");
            if(Input.GetKeyDown (KeyCode.Space)) {
                ShowGlider(true);
                animator.SetBool("Gliding", true);
            }
            if(Input.GetKeyUp(KeyCode.Space)) {
                ShowGlider(false);
                animator.SetBool("Gliding", false);
            }
        }
        if(blockInFrontOfUs && blockInFrontOfUs.CheckIfStuck()){
            blockInFrontOfUs.ReleaseForgetDir();
            grabbingBlock = false;
        }
        if(blockInFrontOfUs == null && grabbingBlock) {
            grabbingBlock = false;
        }

        if(playerFell == true) // respawning
        {
            transform.position = RespawnLocation.position;
            playerFell = false;
        }


        if (carryingLV4CubeKey == true) // managing key for LV4
        {
            LV4CubeKey.SetActive(true);
        } else
        {
            LV4CubeKey.SetActive(false);
        }

        if (LevelTowerSelection.UIElevButtonBottom)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                LevelTowerSelection.TowerElevatorUI.SetActive(false);
                towerMonolithMove.LowerMonolith();
                TowerElevatorScript.chosenLevel = 1;
                LevelTowerSelectionScript.selectedLevel = true;
            }
        }

        if (LevelTowerSelection.UIElevButtonMiddle)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                LevelTowerSelection.TowerElevatorUI.SetActive(false);
                towerMonolithMove.LowerMonolith();
                TowerElevatorScript.chosenLevel = 2;
                LevelTowerSelectionScript.selectedLevel = true;
            }
        }

        if (LevelTowerSelection.UIElevButtonTop)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                LevelTowerSelection.TowerElevatorUI.SetActive(false);
                towerMonolithMove.LowerMonolith();
                TowerElevatorScript.chosenLevel = 3;
                LevelTowerSelectionScript.selectedLevel = true;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuON = !pauseMenuON;
            Debug.Log("pause menu: " + pauseMenuON);
            if (pauseMenuON)
            {
                PauseMenu.SetActive(true);
                Debug.Log("Pause menu up!");
                Time.timeScale = 0;
            } else
            {
                PauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            
        }
            

        DebugFunction(); // debug
    } // end of Update

    private void DebugFunction ()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = TeleportDebugLocation.position;
            Debug.Log("teleproting Debug");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefs.SetInt("usedKey1", 1);
            Debug.Log("used Key One");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefs.SetInt("usedKey2", 1);
            Debug.Log("used Key Two");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("usedKey3", 1);
            Debug.Log("used Key Three");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayerPrefs.SetInt("usedKey4", 1);
            Debug.Log("used Key Four");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
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

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("forgetting all player pref - reloding scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("tabletstory1");
            PlayerPrefs.SetInt("seenTablet1", 1);
            sotryTabletUICount.UpdateUIStoryCount();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("tabletstory2");
            PlayerPrefs.SetInt("seenTablet2", 1);
            sotryTabletUICount.UpdateUIStoryCount();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("tabletstory3");
            PlayerPrefs.SetInt("seenTablet3", 1);
            sotryTabletUICount.UpdateUIStoryCount();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("tabletstory4");
            PlayerPrefs.SetInt("seenTablet4", 1);
            sotryTabletUICount.UpdateUIStoryCount();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("tabletstory5");
            PlayerPrefs.SetInt("seenTablet5", 1);
            sotryTabletUICount.UpdateUIStoryCount();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("tabletstory6");
            PlayerPrefs.SetInt("seenTablet6", 1);
            sotryTabletUICount.UpdateUIStoryCount();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("tabletstory7");
            PlayerPrefs.SetInt("seenTablet7", 1);
            sotryTabletUICount.UpdateUIStoryCount();
        }
    }


    private IEnumerator AnimCallbackJump() {
        yield return new WaitForSeconds(jumpDelay);
        rb.AddForce (Vector3.up * 350); // player jump
        animator.ResetTrigger("Land");
    }

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
        
        ConsoleKeyManager CKMScript = consoleKeyGO.GetComponent<ConsoleKeyManager>();
        if(CKMScript) {
            CKMScript.UseKey(keyNumber);
            for (int i = 0; i < CarryingKey.Length; i++)
            {
                //bool isHeld = PlayerPrefs.GetInt("holdKey" + (i + 1), 0) == 1;
                CarryingKey[i].SetActive(false);
            }
            Debug.Log("key Used");
        } else {
            Debug.LogWarning("console is missing a ConsoleKeyManager");
        }

    }

    void OnCollisionEnter (Collision collision) {
        ConsoleKeyManager CKMScript = collision.gameObject.GetComponent<ConsoleKeyManager>();
        if(CKMScript == null) {
  

            return;
        }
       
        for (int i = 0; i < CarryingKey.Length; i++)
        {
            bool isHeld = PlayerPrefs.GetInt("holdKey" + (i+1), 0) == 1;
            if (isHeld)
            {
                InsertKey((i+1), collision.gameObject);
            }
        }        
    }

    void CalculateMaximumForwardSpeed()
    {
        maximumForwardSpeed = baseMovementSpeed * runningSpeedMultiplier;
    }

    void OnValidate() {
        CalculateMaximumForwardSpeed();
    }
} // end of class
