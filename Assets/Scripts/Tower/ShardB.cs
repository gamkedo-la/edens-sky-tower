using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardB : MonoBehaviour
{
    public bool shardBActivated = false;
    public Transform ShardBEngagement;
    Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        if(shardBActivated) {
            gameObject.transform.position = ShardBEngagement.position;
        }      
    }

}
