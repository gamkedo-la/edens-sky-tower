using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getKey : MonoBehaviour
{
    public string keyNumber;
    bool snapToPlayer = false;
    bool snapToKeyHole = false;
    public Transform playerItemHolding;
    public Transform itemToConsole;

    GameObject keyhole;  
    GameObject player;
    

    void Awake() {
        keyhole = GameObject.Find("TowerShards/Shard" + keyNumber);
        player = GameObject.Find("PLAYER");
    }


    void Start()
    {

        
    }

    void Update()
    {
        if (snapToPlayer) {
            gameObject.transform.position = playerItemHolding.transform.position;
        }

        if (snapToKeyHole) {
            gameObject.transform.position = itemToConsole.transform.position;
            PlayerPrefs.SetInt("usedKey" + keyNumber, 1);
            //keyholeA.GetComponent<TowerB>().keyHoleAActivated = true; // makes rings move

            snapToPlayer = false;
            snapToKeyHole = false;
        }
    }

    void OnCollisionEnter (Collision collision) {
        Debug.Log("collision registered!!!");

        if (collision.gameObject.CompareTag ("Player")) {
            Debug.Log("Got Key!");
            snapToPlayer = true;
        }

        if (collision.gameObject.CompareTag ("KeyHoleA")) {
            Debug.Log("Key Inserted!");
            snapToPlayer = false;
            snapToKeyHole = true;
        }

    }
}
