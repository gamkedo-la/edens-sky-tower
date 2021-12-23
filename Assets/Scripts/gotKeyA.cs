using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotKeyA : MonoBehaviour
{
    public Transform playerTransform;
    public Transform keyHoleATransform;
    bool snapToPlayer = false;
    bool snapToKeyHole = false;

    GameObject keyholeA;  
    GameObject Player;
    

    void Awake() {
        keyholeA = GameObject.Find("Tower/TowerSegmentB");
        Player = GameObject.Find("PLAYER");
    }


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Key Script");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (snapToPlayer) {
            gameObject.transform.position = playerTransform.position;
        }

        if (snapToKeyHole) {
            gameObject.transform.position = keyHoleATransform.position;
            keyholeA.GetComponent<TowerB>().keyHoleAActivated = true;
            Debug.Log("Player Transport");
            Player.GetComponent<P2Class>().PlayerTransferShard1 = true;

            snapToPlayer = false;
            snapToKeyHole = false;
        }
    }

    void OnCollisionEnter (Collision collision) {
        Debug.Log("collision registered!");

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
