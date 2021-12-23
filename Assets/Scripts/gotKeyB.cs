using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotKeyB : MonoBehaviour
{
    public Transform playerTransform;
    public Transform keyHoleBTransform;
    bool snapToPlayer = false;
    bool snapToKeyHole = false;

    GameObject keyholeB;  
    

    void Awake() {
        //keyholeB = GameObject.Find("Tower/TowerSegmentC");
        keyholeB = GameObject.Find("TowerShards/Shard2");
    }


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Key Script");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (snapToPlayer) {
            gameObject.transform.position = playerTransform.position;
        }

        if (snapToKeyHole) {
            gameObject.transform.position = keyHoleBTransform.position;
            keyholeB.GetComponent<ShardB>().keyHoleBActivated = true;

            snapToPlayer = false;
            snapToKeyHole = false;
        }
    }

    void OnCollisionEnter (Collision collision) {
        // Debug.Log("collision registered!");

        if (collision.gameObject.CompareTag ("Player")) {
            Debug.Log("Got Key!");
            snapToPlayer = true;
        }

        if (collision.gameObject.CompareTag ("KeyHoleB")) {
            Debug.Log("collision registered!");
            Debug.Log("Key Inserted!");
            snapToPlayer = false;
            snapToKeyHole = true;
            
        }

    }
}
