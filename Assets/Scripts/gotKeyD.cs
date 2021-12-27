using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotKeyD : MonoBehaviour
{
    public Transform playerTransform;
    public Transform keyHoleDTransform;
    bool snapToPlayer = false;
    bool snapToKeyHole = false;

    GameObject Player;
    
    void Awake() {
        Player = GameObject.Find("PLAYER");
    }

    void Update()
    {
        if (snapToPlayer) {
            gameObject.transform.position = playerTransform.position;
        }

        if (snapToKeyHole) {
            gameObject.transform.position = keyHoleDTransform.position;
           
            snapToPlayer = false;
            snapToKeyHole = false;
        }
    }

    void OnCollisionEnter (Collision collision) {

        if (collision.gameObject.CompareTag ("Player")) {
            Debug.Log("Got Key!");
            snapToPlayer = true;
        }

        if (collision.gameObject.CompareTag ("KeyHoleD")) {
            Debug.Log("Key Inserted!");
            snapToPlayer = false;
            snapToKeyHole = true;
            
        }

    }
}
