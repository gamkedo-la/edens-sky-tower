using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NESWPushable : MonoBehaviour
{
    private float pushAmount = 1.0f;
    private int directionLock = -1;
    private bool blockStuck = false;

    public void ReleaseForgetDir(){
        directionLock = -1;
        blockStuck = false;
    }
    public bool CheckIfStuck() {
        return blockStuck;
    }
    public void PushOrPull(Transform pusherPos) {
        if(blockStuck) {
            return;
        }
        float pusherDist = Vector3.Distance(transform.position, pusherPos.position);
        float pusherDistChange = 3.0f - pusherDist;
       
        Vector3 flatPlayerPosition = pusherPos.position;
        flatPlayerPosition.y = transform.position.y;
        float angBetween = Mathf.Atan2(flatPlayerPosition.z-transform.position.z, flatPlayerPosition.x-transform.position.x) * Mathf.Rad2Deg;
        float pullingOrPushing = 0.0f;
       /*  if(pusherDistChange < 0.0f) {
            pullingOrPushing = -1.0f;
        } else if (pusherDistChange > 0.0f) {
            pullingOrPushing = 1.0f;
        } */
        pullingOrPushing = pusherDistChange;
        Debug.Log(angBetween);
        int dirNow = -1;
        if(angBetween > -45.0f && angBetween < 45.0f){
            Debug.Log("push west");
            transform.position += Vector3.left * pushAmount * pullingOrPushing;
            dirNow = 0;
        } else if(angBetween > 45.0f && angBetween < 135.0f){
            Debug.Log("push south");
            transform.position += Vector3.back * pushAmount * pullingOrPushing;
            dirNow = 1;
        } else if(angBetween < -45.0f && angBetween > -135.0f){
            Debug.Log("push north");
            transform.position += Vector3.forward * pushAmount * pullingOrPushing;
            dirNow = 2;
        } else {
            Debug.Log("push east");
            transform.position += Vector3.right * pushAmount * pullingOrPushing;
            dirNow = 3;
        }
        
        if(directionLock == -1) {
            directionLock = dirNow;
        } else if (directionLock != dirNow) {
            blockStuck = true;
        }
    }

}
