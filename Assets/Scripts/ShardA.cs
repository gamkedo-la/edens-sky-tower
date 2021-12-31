using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardA : MonoBehaviour
{
    bool activeTowerKey;
    float originalPos;
    bool towerToMoveDown = true;

    GameObject TowerKey;

    void Awake() {
        //TowerKey = GameObject.Find("TowerShards/Shard3/Platform3/KeyD");
        TowerKey = GameObject.Find("KeyD");
    }

    void Start()
    {
        originalPos = transform.position.y;
    }

    void Update()
    {
        activeTowerKey = TowerKey.GetComponent<gotKeyD>().towerKey;
        Debug.Log(activeTowerKey);

        if(activeTowerKey) {
            if(towerToMoveDown) {
                transform.position += transform.up * Time.deltaTime * -3.0f;
            }             
        }
    }

    void OnCollisionEnter (Collision col) {
        Debug.Log("tower to stop descent");
        // if(col.gameObject.CompareTag ("Ring0")) {
             
             towerToMoveDown = false;
        // }
    }
}
