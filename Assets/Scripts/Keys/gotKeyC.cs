using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotKeyC : MonoBehaviour
{
    public Transform playerTransform;
    public Transform keyHoleCTransform;
    bool snapToPlayer = false;
    bool snapToKeyHole = false;

    GameObject keyholeC;  
    GameObject Player;
    

    void Awake() {
        keyholeC = GameObject.Find("TowerShards/Shard3");
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
            gameObject.transform.position = keyHoleCTransform.position;
            keyholeC.GetComponent<ShardC>().keyHoleCActivated = true;
            Player.GetComponent<Player>().PlayerTransferShard3 = true;

            snapToPlayer = false;
            snapToKeyHole = false;
        }
    }

    void OnCollisionEnter (Collision collision) {

        if (collision.gameObject.CompareTag ("Player")) {
            snapToPlayer = true;
        }

        if (collision.gameObject.CompareTag ("KeyHoleC")) {
            snapToPlayer = false;
            snapToKeyHole = true;
            
        }

    }
}
