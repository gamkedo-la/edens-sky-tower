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

    void Start(){
        instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void LateUpdate() {
        CamControl();
    }


    void CamControl(){
        float smoothRate = 0.2f;
        if(cameraPositionOverride != null) {
            smoothRate = 0.002f;
            transform.position = Vector3.Lerp(transform.position, cameraPositionOverride.position, smoothRate);
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraPositionOverride.rotation, smoothRate);
            return;
        }
        //mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 180);

        //transform.LookAt(target);
        
        //Vector3 cameraTurn = cameraPivot.rotation.ToEulerAngles();
        //cameraPivot.rotation = Quaternion.Euler(mouseY, cameraTurn.y, cameraTurn.z);

        transform.position = Vector3.Lerp(transform.position, target.position, smoothRate);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, smoothRate);
        //player.rotation = Quaternion.Euler(0, mouseX, 0);
    }

}
