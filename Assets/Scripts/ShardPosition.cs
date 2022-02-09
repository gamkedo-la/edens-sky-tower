using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardPosition : MonoBehaviour
{
    public string keyNumber;
    private bool shardActivated = false;
    public Transform ShardEngagementDestination;
    private Vector3 startLocalPosition;
    public bool ControllingShardA = false;

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

    void OnCollisionEnter (Collision collision) {

        Debug.Log ("Colliding with console!");

        //checking collision to control shards

        if(collision.gameObject.CompareTag ("Player")) {
             ControllingShardA = true;
         } else {
             ControllingShardA = false;
         }

         Debug.Log ("Colliding with Console A " + ControllingShardA);

    }

}
