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
    GameObject Player;
    

    void Awake() {
        keyholeB = GameObject.Find("Environment/TowerShards/TowerModelShardB");
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
            gameObject.transform.position = keyHoleBTransform.position;
            keyholeB.GetComponent<ShardB>().shardBActivated = true;
            Player.GetComponent<Player>().PlayerTransferShard2 = true;
            

            snapToPlayer = false;
            snapToKeyHole = false;
        }
    }

    void OnCollisionEnter (Collision collision) {

        if (collision.gameObject.CompareTag ("Player")) {
            snapToPlayer = true;
        }

        if (collision.gameObject.CompareTag ("KeyHoleB")) {
            snapToPlayer = false;
            snapToKeyHole = true;
            
        }

    }
}
