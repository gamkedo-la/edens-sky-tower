using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerElevator : MonoBehaviour
{
    public Transform[] wayPointList;
    private Player playerScript;
    public bool inMotion = false;
    float progress = 0.0f;
    float progressPace = 0.05f;
    int goToWayPoint;
    public towerMonolithMove towerMonolithMove;
    public float chosenLevel = 0;
    public GameObject shardOneElevCollider;
    public GameObject shardTwoElevCollider;
    public GameObject shardThreeElevCollider;


    public GameObject ElevatorPlatform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inMotion)
        {
            progress += Time.deltaTime * progressPace;
            if (progress > 1.0f)
            {
                inMotion = false;
                
                //playerScript.enabled = true;
            }
            else
            {
                //playerScript.transform.position = Vector3.Lerp(wayPointList[0].position, wayPointList[goToWayPoint].position, progress);
                ElevatorPlatform.transform.position = Vector3.Lerp(wayPointList[0].position, wayPointList[goToWayPoint].position, progress);
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        //checking collision to control objects
        towerMonolithMove.RiseMonolith();
        //bool firstKey = PlayerPrefs.GetInt("usedKey1", 0) == 1;
        if (/*firstKey*/ chosenLevel == 1 && collision.gameObject.CompareTag("Player"))
        {

            //playerScript = collision.gameObject.GetComponent<Player>();
            //playerScript.enabled = false;
            //Rigidbody playerRb = playerScript.GetComponent<Rigidbody>();
            //playerRb.useGravity = false;
            ////inMotion = true;
            progress = 0.0f;
            //bool secondKey = PlayerPrefs.GetInt("usedKey2", 0) == 1;
            //bool thirdKey = PlayerPrefs.GetInt("usedKey3", 0) == 1;
            goToWayPoint = 1;
            shardOneElevCollider.SetActive(true);
        } 
        else
        {
            shardOneElevCollider.SetActive(false);
        }

        if (chosenLevel == 2 && collision.gameObject.CompareTag("Player"))
        {
            ////inMotion = true;
            progress = 0.0f;
            goToWayPoint = 2;
            shardTwoElevCollider.SetActive(true);
        }
        else
        {
            shardTwoElevCollider.SetActive(false);
        }

        if (chosenLevel == 3 && collision.gameObject.CompareTag("Player"))
        {
            ////inMotion = true;
            progress = 0.0f;
            goToWayPoint = 3;
            shardThreeElevCollider.SetActive(true);
        }
        else
        {
            shardThreeElevCollider.SetActive(false);
        }

    }
}