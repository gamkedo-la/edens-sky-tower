using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardRotation : MonoBehaviour
{

    bool ControllingShardA = false;

    GameObject shard;


    void Awake() {
        shard = GameObject.Find("TowerShards/TowerModelShardA");
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ControllingShardA) {
            if (Input.GetKey (KeyCode.A)) {
            shard.transform.Rotate(0, 01, 0, Space.Self);
            }

            if (Input.GetKey (KeyCode.D)) {
            shard.transform.Rotate(0, -01, 0, Space.Self);
            }
        }
    }

    void OnCollisionEnter (Collision collision) {

        //Debug.Log ("Colliding with console!");

        //checking collision to control shards

        if(collision.gameObject.CompareTag ("Player")) {
             ControllingShardA = true;
         } else {
             ControllingShardA = false;
         }

         //Debug.Log ("Colliding with Console A " + ControllingShardA);

    }
}
