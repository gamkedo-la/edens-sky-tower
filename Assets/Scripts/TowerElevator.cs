using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerElevator : MonoBehaviour
{
    public Transform [] wayPointList;
    private Player playerScript;
    bool inMotion = false;
    float progress = 0.0f;
    float progressPace = 0.5f;

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
                playerScript.enabled = true;
            } else {
                playerScript.transform.position = Vector3.Lerp(wayPointList[0].position, wayPointList[1].position, progress);
            }
        }
        
    }

    void OnCollisionEnter (Collision collision) {
        //checking collision to control objects

        if(collision.gameObject.CompareTag ("Player")) {
            playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.enabled = false;
            inMotion = true;
            progress = 0.0f;
        }
    }
}
