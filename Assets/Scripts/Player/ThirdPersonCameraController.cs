using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{

    public float rotationSpeed = 1;
    public Transform target, player, cameraPivot;
    float mouseX = 0f, mouseY = 0f;


    void Start(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void LateUpdate() {
        CamControl();
    }


    void CamControl(){
        //mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY += Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 180);

        //transform.LookAt(target);
        float smoothRate = 0.2f;
        //Vector3 cameraTurn = cameraPivot.rotation.ToEulerAngles();
        //cameraPivot.rotation = Quaternion.Euler(mouseY, cameraTurn.y, cameraTurn.z);

        transform.position = Vector3.Lerp(transform.position, target.position, smoothRate);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, smoothRate);
        //player.rotation = Quaternion.Euler(0, mouseX, 0);
    }

}
