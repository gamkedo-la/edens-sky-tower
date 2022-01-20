using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardC : MonoBehaviour
{
    public bool shardCEngaged = false;
    public bool keyHoleCActivated = false;
    bool startDescent = false;
    float originalPos;

    GameObject Player;

    void Awake() {
        Player = GameObject.Find("PLAYER");
    }

    void Start()
    {
        originalPos = transform.position.y;
    }

    void Update()
    {
        if(keyHoleCActivated) {
            startDescent = true; 
        }

        if(!shardCEngaged && startDescent) {
            transform.position += transform.up * Time.deltaTime * -3.0f;
            }
    }

    void OnCollisionEnter (Collision collision) {
        Debug.Log ("Shard C Colliding!");
        if(collision.gameObject.CompareTag ("Shard2")) {
            shardCEngaged = true;
            startDescent = false;
            Player.GetComponent<Player>().PlayerTransferShard3 = true;
        }
    }
}
