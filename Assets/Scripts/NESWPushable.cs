using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NESWPushable : MonoBehaviour
{
    private float pushAmount = 1.0f;
    private int directionLock = -1;
    private bool blockStuck = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    public void ReleaseForgetDir(){
        directionLock = -1;
        blockStuck = false;
    }
    public bool CheckIfStuck() {
        return blockStuck;
    }
    public bool PushOrPull(Transform pusherPos) {
        if(blockStuck) {
            return false;
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
        int dirNow = -1;
        if(angBetween > -45.0f && angBetween < 45.0f){
            //Debug.Log("push west");
            transform.position += Vector3.left * pushAmount * pullingOrPushing;
            dirNow = 0;
        } else if(angBetween > 45.0f && angBetween < 135.0f){
            //Debug.Log("push south");
            transform.position += Vector3.back * pushAmount * pullingOrPushing;
            dirNow = 1;
        } else if(angBetween < -45.0f && angBetween > -135.0f){
            //Debug.Log("push north");
            transform.position += Vector3.forward * pushAmount * pullingOrPushing;
            dirNow = 2;
        } else {
            //Debug.Log("push east");
            transform.position += Vector3.right * pushAmount * pullingOrPushing;
            dirNow = 3;
        }

        if (directionLock == -1)
        {
            directionLock = dirNow;
        }
        else if (directionLock != dirNow)
        {
            blockStuck = true;
        }

        RaycastHit rhInfo;
        float measureDown = 1.75f;
        float cornerOffset = 1.48f;
        bool frontLeft = Physics.Raycast(transform.position + Vector3.forward * cornerOffset - Vector3.right * cornerOffset, Vector3.down, out rhInfo, measureDown) == false;
        bool frontRight = Physics.Raycast(transform.position + Vector3.forward * cornerOffset + Vector3.right * cornerOffset, Vector3.down, out rhInfo, measureDown) == false;
        bool backLeft = Physics.Raycast(transform.position - Vector3.forward * cornerOffset - Vector3.right * cornerOffset, Vector3.down, out rhInfo, measureDown) == false;
        bool backRight = Physics.Raycast(transform.position - Vector3.forward * cornerOffset + Vector3.right * cornerOffset, Vector3.down, out rhInfo, measureDown) == false;

        if (frontLeft && frontRight && backLeft && backRight) { //drop check
            Debug.Log("Below block empty");
            transform.position += -Vector3.up * Time.deltaTime * 10.0f;
            rb.isKinematic = false;
            ReleaseForgetDir();
            return true;
        }
        return false;
        
    }

}
