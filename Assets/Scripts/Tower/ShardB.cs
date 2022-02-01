using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardB : MonoBehaviour
{
    public bool shardBActivated = false;
    public Transform ShardBEngagement;

    public bool shardBEngaged = false;
    public bool keyHoleBActivated = false;
    bool startDescent = false;
    float originalPos;
    Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        originalPos = transform.position.y;
    }

    void Update()
    {
        if(shardBActivated) {
            gameObject.transform.position = ShardBEngagement.position;
        }      
    }

}
