using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardB : MonoBehaviour
{
    public bool shardBEngaged = false;
    public bool keyHoleBActivated = false;
    bool startDescent = false;
    float originalPos;

    GameObject Player;

    void Awake() {
        Player = GameObject.Find("PLAYER");
    }

    void Start()
    {
        //Debug.Log("ShardB Script!");
        originalPos = transform.position.y;
    }

    void Update()
    {
        if(keyHoleBActivated) {
            startDescent = true; 
        }

        if(!shardBEngaged && startDescent) {
            transform.position += transform.up * Time.deltaTime * -3.0f;
            }
    }

    void OnCollisionEnter (Collision collision) {
        if(collision.gameObject.CompareTag ("Shard1")) {
            shardBEngaged = true;
            startDescent = false;
            Player.GetComponent<P2Class>().PlayerTransferShard2 = true;
        }
    }
}
