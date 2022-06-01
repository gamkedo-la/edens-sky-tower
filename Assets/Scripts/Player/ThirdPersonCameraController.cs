using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public static ThirdPersonCameraController instance;
    public float rotationSpeed = 1;
    public Transform target, player, cameraPivot;
    float mouseX = 0f, mouseY = 0f;
    public Transform cameraPositionOverride = null;
    public bool sharpTransition = false;
    LayerMask ignorePlayer;

    void Start(){
        instance = this;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = target.position;
        transform.rotation = target.rotation;
        ignorePlayer = ~LayerMask.GetMask("PlayerCharacter");
    }

    void LateUpdate() {
        if(Time.timeScale > 0.0f) {
            CamControl();
        }        
    }


    void CamControl(){
        float smoothRate = 0.03f;

        

        //Debug.Log(smoothRate);

        if(cameraPositionOverride != null) {
            
            if (sharpTransition == true)
            {
                smoothRate = 1.0f; // used for elevator view
            } else
            {
                smoothRate = 0.02f; // used for transitioning to controlling tower shard view
            }
            transform.position = Vector3.Lerp(transform.position, cameraPositionOverride.position, smoothRate);
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraPositionOverride.rotation, smoothRate);
            return;
        }
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -15, 70);
        RaycastHit rhInfo;
        Vector3 direction =  target.position - cameraPivot.position;
        if (Physics.Raycast(cameraPivot.position, direction, out rhInfo, direction.magnitude, ignorePlayer))
        {
            Debug.Log(rhInfo.collider.gameObject.name);
            transform.position = rhInfo.point;
        }
        else
        {
            transform.position = target.position;
        }
        
        transform.rotation = target.rotation;
        //transform.LookAt(target);

        //Vector3 cameraTurn = cameraPivot.rotation.ToEulerAngles();
        //cameraPivot.rotation = Quaternion.Euler(mouseY, cameraTurn.y, cameraTurn.z);

        ////transform.position = Vector3.Lerp(transform.position, target.position, smoothRate);
        ////transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, smoothRate);
        cameraPivot.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }

}
