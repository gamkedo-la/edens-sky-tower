using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardC : MonoBehaviour
{
    public bool shardCActivated = false;
    public Transform ShardCEngagement;
    Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        if(shardCActivated) {
            gameObject.transform.position = ShardCEngagement.position;
        }      
    }

}
