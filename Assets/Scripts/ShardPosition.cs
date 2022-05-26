using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardPosition : MonoBehaviour
{
    public string keyNumber;
    private bool shardActivated = false;
    public Transform ShardEngagementDestination;
    private Vector3 startLocalPosition;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        shardActivated = PlayerPrefs.GetInt("usedKey" + keyNumber, 0) ==1;

        if(shardActivated) {
            gameObject.transform.position = ShardEngagementDestination.position;
        } else {
            gameObject.transform.localPosition = startLocalPosition;
        }  

    }

}
