using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NESWPushable : MonoBehaviour
{
    private float pushAmount = 1.0f;
    private int directionLock = -1;
    private bool blockStuck = false;
    private Rigidbody rb;
    private Collider _collider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        _collider = GetComponent<MeshCollider>();
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

        Vector3 flatPlayerPosition = pusherPos.position;
        Vector3 pushablePosition = transform.position;
        float pusherDist = Vector3.Distance(pushablePosition, flatPlayerPosition);
        float pusherDistChange = 3.0f - pusherDist;
       
        flatPlayerPosition.y = pushablePosition.y;
        float angBetween = Mathf.Atan2(flatPlayerPosition.z-pushablePosition.z, flatPlayerPosition.x-pushablePosition.x) * Mathf.Rad2Deg;
        float pullingOrPushing = 0.0f;
       /*  if(pusherDistChange < 0.0f) {
            pullingOrPushing = -1.0f;
        } else if (pusherDistChange > 0.0f) {
            pullingOrPushing = 1.0f;
        } */
        pullingOrPushing = pusherDistChange;
        int dirNow = -1;
        var colliderBounds = _collider.bounds;
        var offsetForCollisionCheck = (colliderBounds.extents.y - .1f)*Vector3.up;
        if(angBetween > -45.0f && angBetween < 45.0f){
            // Debug.Log("push west");
            var collisionCheckMiddle = pushablePosition - offsetForCollisionCheck + colliderBounds.extents.x * Vector3.left;
            var collisionCheckSide1 = collisionCheckMiddle  + colliderBounds.extents.z * Vector3.back;
            var collisionCheckSide2 = collisionCheckMiddle + colliderBounds.extents.z * Vector3.forward;
            bool collision = pullingOrPushing >= 0  && (Physics.Raycast(collisionCheckMiddle , Vector3.left, pushAmount * .2f)
                                                    || Physics.Raycast(collisionCheckSide1, Vector3.left, pushAmount * .2f)
                                                    || Physics.Raycast(collisionCheckSide2 , Vector3.left, pushAmount * .2f));
            if(!collision)
                rb.MovePosition(pushablePosition + pushAmount * pullingOrPushing * Vector3.left);
            dirNow = 0;
        } else if (angBetween > 45.0f && angBetween < 135.0f)
        {
            // Debug.Log("push south");
            var collisionCheckMiddle = pushablePosition - offsetForCollisionCheck + colliderBounds.extents.z * Vector3.back;
            var collisionCheckSide1 = collisionCheckMiddle + colliderBounds.extents.x * Vector3.left;
            var collisionCheckSide2 = collisionCheckMiddle + colliderBounds.extents.x * Vector3.right;
            bool collision = pullingOrPushing >= 0  && (Physics.Raycast(collisionCheckMiddle , Vector3.back, pushAmount * .2f)
                                                    || Physics.Raycast(collisionCheckSide1, Vector3.back, pushAmount * .2f)
                                                    || Physics.Raycast(collisionCheckSide2, Vector3.back, pushAmount * .2f));
            if(!collision)
                rb.MovePosition(pushablePosition + pushAmount * pullingOrPushing * Vector3.back);
            dirNow = 1;
        } else if(angBetween < -45.0f && angBetween > -135.0f){
            // Debug.Log("push north");
            var collisionCheckMiddle = pushablePosition - offsetForCollisionCheck + colliderBounds.extents.z * Vector3.forward;
            var collisionCheckSide = collisionCheckMiddle + colliderBounds.extents.x * Vector3.right;
            var collisionCheckSide2 = collisionCheckMiddle + colliderBounds.extents.x * Vector3.left;
            Debug.DrawLine(collisionCheckSide, collisionCheckSide + Vector3.back);
            bool collision = pullingOrPushing >= 0  && (Physics.Raycast(collisionCheckMiddle , Vector3.forward, pushAmount * .2f)
                                                    || Physics.Raycast(collisionCheckSide, Vector3.forward, pushAmount * .2f)
                                                    || Physics.Raycast(collisionCheckSide2, Vector3.forward, pushAmount * .2f));
            if(!collision)
                rb.MovePosition(pushablePosition + pushAmount * pullingOrPushing * Vector3.forward);
            dirNow = 2;
        } else {
            // Debug.Log("push east");
            var collisionCheckMiddle = pushablePosition - offsetForCollisionCheck + colliderBounds.extents.x * Vector3.right;
            var collisionCheck1 = collisionCheckMiddle + colliderBounds.extents.z * Vector3.back;
            var collisionCheck2 = collisionCheckMiddle + colliderBounds.extents.z * Vector3.forward;
            bool collision = pullingOrPushing >= 0  && (Physics.Raycast(collisionCheckMiddle , Vector3.right, pushAmount * .2f)
                                                    || Physics.Raycast(collisionCheck1, Vector3.right, pushAmount * .2f)
                                                    || Physics.Raycast(collisionCheck2, Vector3.right, pushAmount * .2f));
            if(!collision)
                rb.MovePosition(pushablePosition + pushAmount * pullingOrPushing * Vector3.right);
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
        bool frontLeft = Physics.Raycast(pushablePosition + Vector3.forward * cornerOffset - Vector3.right * cornerOffset, Vector3.down, out rhInfo, measureDown) == false;
        bool frontRight = Physics.Raycast(pushablePosition + Vector3.forward * cornerOffset + Vector3.right * cornerOffset, Vector3.down, out rhInfo, measureDown) == false;
        bool backLeft = Physics.Raycast(pushablePosition - Vector3.forward * cornerOffset - Vector3.right * cornerOffset, Vector3.down, out rhInfo, measureDown) == false;
        bool backRight = Physics.Raycast(pushablePosition - Vector3.forward * cornerOffset + Vector3.right * cornerOffset, Vector3.down, out rhInfo, measureDown) == false;

        if (frontLeft && frontRight && backLeft && backRight) { //drop check
            Debug.Log("Below block empty");
            transform.position += -Time.deltaTime * 10.0f * Vector3.up;
            rb.isKinematic = false;
            ReleaseForgetDir();
            return true;
        }
        return false;
        
    }
}
