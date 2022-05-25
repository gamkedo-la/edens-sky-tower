using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getKey : MonoBehaviour
{
    public string keyNumber;
    public Transform playerItemHolding;
    //public Transform itemToConsole;

    GameObject keyhole;  
    GameObject player;
    

    void Awake() {
        keyhole = GameObject.Find("TowerShards/Shard" + keyNumber);
        player = GameObject.Find("PLAYER");
        bool isHeld = PlayerPrefs.GetInt("holdKey" + keyNumber, 0) == 1;
        bool isUsed = PlayerPrefs.GetInt("usedKey" + keyNumber, 0) == 1;
        if(isHeld || isUsed) {
            gameObject.SetActive (false);
        }
    }

    void OnCollisionEnter (Collision collision) {
        Debug.Log("collision registered!!!");

        if (collision.gameObject.CompareTag ("Player")) {
            Debug.Log("Got Key!");
            gameObject.SetActive(false);
            Player player = collision.gameObject.GetComponent<Player>(); 
            player.GetKey(keyNumber);
        }

    }
}
