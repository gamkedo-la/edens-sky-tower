using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardB : MonoBehaviour
{
    public bool shardBEngaged = false;
    public bool keyHoleBActivated = false;
    bool startDescent = false;
    float originalPos;
    Rigidbody rb;

    GameObject Player;

    void Awake() {
        Player = GameObject.Find("PLAYER");
        rb = GetComponent<Rigidbody>();
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
            //rb.velocity = transform.up * 3.0f;
        } else {
            //rb.velocity = Vector3.zero;
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
