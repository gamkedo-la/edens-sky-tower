using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float  speed = 2.0f;
    public float farEnoughToMoveTo = 0.01f;
    Vector3 gotoPt;

    // Start is called before the first frame update
    void Start()
    {
        gotoPt = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Mouse Clicked!");

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rhInfo;
            int mouseMask = ~LayerMask.GetMask("PlayerCharacter");

            if (Physics.Raycast(mouseRay, out rhInfo, 500.0f, mouseMask)) {
                Debug.Log("Mouse ray hit:" + rhInfo.collider.gameObject.name + " at " + rhInfo.point);

                //transform.position = rhInfo.point + Vector3.up * 0.5f;
                gotoPt = rhInfo.point;
                gotoPt.y = transform.position.y;
            }
            else
            {
                Debug.Log("Mouse ray hit nothing!");
            }
        } //end of if mouse input

        float distToPoint = Vector3.Distance(transform.position, gotoPt);
        if (distToPoint > farEnoughToMoveTo) {
            
            transform.LookAt(gotoPt);
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else 
        {
            transform.position = gotoPt;
        }
    } // end of Update
} // end of class
