using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotKeyC : MonoBehaviour
{
    public Transform playerTransform;
    public Transform keyHoleCTransform;
    bool snapToPlayer = false;
    bool snapToKeyHole = false;

    GameObject keyholeC;  
    

    void Awake() {
        keyholeC = GameObject.Find("Tower/TowerSegmentD");
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
            gameObject.transform.position = keyHoleCTransform.position;
            keyholeC.GetComponent<TowerD>().keyHoleCActivated = true;

            snapToPlayer = false;
            snapToKeyHole = false;
        }
    }

    void OnCollisionEnter (Collision collision) {
        // Debug.Log("collision registered!");

        if (collision.gameObject.CompareTag ("Player")) {
            Debug.Log("Got Key C!");
            snapToPlayer = true;
        }

        if (collision.gameObject.CompareTag ("KeyHoleC")) {
            Debug.Log("collision registered!");
            Debug.Log("Key Inserted!");
            snapToPlayer = false;
            snapToKeyHole = true;
            
        }

    }
}
