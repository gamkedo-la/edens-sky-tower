using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NESWPushable : MonoBehaviour
{
    private float pushAmount = 0.2f;

    void OnCollisionEnter(Collision coll) {
        if(coll.gameObject.tag == "Player") {
            Vector3 flatPlayerPosition = coll.gameObject.transform.position;
            flatPlayerPosition.y = transform.position.y;
            float angBetween = Mathf.Atan2(flatPlayerPosition.z-transform.position.z, flatPlayerPosition.x-transform.position.x) * Mathf.Rad2Deg;
            Debug.Log(angBetween);
            if(angBetween > -45.0f && angBetween < 45.0f){
                Debug.Log("push west");
                transform.position += Vector3.left * pushAmount;
            } else if(angBetween > 45.0f && angBetween < 135.0f){
                Debug.Log("push south");
                transform.position += Vector3.back * pushAmount;
            } else if(angBetween < -45.0f && angBetween > -135.0f){
                Debug.Log("push north");
                transform.position += Vector3.forward * pushAmount;
            } else {
                Debug.Log("push east");
                transform.position += Vector3.right * pushAmount;
            }
        }
    }

}
