using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerElevator : MonoBehaviour
{
    public Transform [] wayPointList;
    private Player playerScript;
    bool inMotion = false;
    float progress = 0.0f;
    float progressPace = 0.2f;
    int goToWayPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inMotion) {
            progress += Time.deltaTime * progressPace;
            if(progress > 1.0f) {
                inMotion = false;
                //playerScript.enabled = true;
            } else {
                playerScript.transform.position = Vector3.Lerp(wayPointList[0].position, wayPointList[goToWayPoint].position, progress);
            }
        }
        
    }

    void OnCollisionEnter (Collision collision) {
        //checking collision to control objects
        bool firstKey = PlayerPrefs.GetInt("usedKey1", 0) == 1;
        if(firstKey && collision.gameObject.CompareTag ("Player")) {
            playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.enabled = false;
            Rigidbody playerRb = playerScript.GetComponent<Rigidbody>();
            playerRb.useGravity = false;
            inMotion = true;
            progress = 0.0f;
            bool secondKey = PlayerPrefs.GetInt("usedKey2", 0) == 1;
            bool thirdKey = PlayerPrefs.GetInt("usedKey3", 0) == 1;

            if(thirdKey) {
                goToWayPoint = 3;
            } else if (secondKey) {
                goToWayPoint = 2;
            } else {
                goToWayPoint = 1;
            } 
        }
    }
}
