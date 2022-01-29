using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotKeyA : MonoBehaviour
{
    public Transform playerTransform;
    public Transform keyHoleATransform;
    bool snapToPlayer = false;
    bool snapToKeyHole = false;

    GameObject keyholeA;  
    GameObject Player;
    

    void Awake() {
        // keyholeA = GameObject.Find("Tower/TowerSegmentB");
        keyholeA = GameObject.Find("TowerShards/Shard1");
        Player = GameObject.Find("PLAYER");
    }


    void Start()
    {

        
    }

    void Update()
    {
        if (snapToPlayer) {
            gameObject.transform.position = playerTransform.position;
        }

        if (snapToKeyHole) {
            gameObject.transform.position = keyHoleATransform.position;
            Player.GetComponent<Player>().PlayerTransferShard1 = true;
            //keyholeA.GetComponent<TowerB>().keyHoleAActivated = true; // makes rings move

            snapToPlayer = false;
            snapToKeyHole = false;
        }
    }

    void OnCollisionEnter (Collision collision) {
        Debug.Log("collision registered!");

        if (collision.gameObject.CompareTag ("Player")) {
            Debug.Log("Got Key!");
            snapToPlayer = true;
        }

        if (collision.gameObject.CompareTag ("KeyHoleA")) {
            Debug.Log("Key Inserted!");
            snapToPlayer = false;
            snapToKeyHole = true;
            
        }

    }
}