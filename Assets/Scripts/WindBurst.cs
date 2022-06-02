using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBurst : MonoBehaviour
{
    [SerializeField] private float windMagnitude = 0.7f;
    [SerializeField] private float durationPlayerLosesControl = 5.0f;
    [SerializeField] private float durationWindAffectsPlayer = 0.2f;
    [SerializeField] private Transform originTransform;

    private Rigidbody rbTarget;

    private Quaternion originalRotation;
    private Quaternion targetRotation;

    private Player player;
    private Vector3 direction;
    public float distanceBetweenFallingPointAndTrigger;

    private bool affectedByWind = false;
    private bool windCameraSpeedOn = false;

    private float rotationPerSecond;  
    private float rotationAmount;
    private float amountRotated;  
    private bool isRotating;  

    void Start () {
        if(rbTarget == null || player == null) {
            //this.enabled = false; // preventing constant error from uninitalized variables
            //Debug.Log("preventing null ref exception while wind burst - WIP");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //rbTarget = other.GetComponent<Rigidbody>();
            player = other.GetComponent<Player>();

            //distanceBetweenFallingPointAndTrigger = Vector3.Distance(player.lastPositionBeforeJumpingOffPlatform, other.transform.position);
            //distanceBetweenFallingPointAndTrigger = distanceBetweenFallingPointAndTrigger < 3.0f ? 3.0f : distanceBetweenFallingPointAndTrigger;
            //direction = (player.lastPositionBeforeJumpingOffPlatform - other.transform.position).normalized;
            //originalRotation = rbTarget.transform.rotation;
            //targetRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);

            //rotationAmount = UnityEngine.Random.Range(180 - 45, 180 + 45);
            //rotationPerSecond = rotationAmount / 2;
            player.transform.position = player.lastPositionBeforeJumpingOffPlatform.position;
            //player.isAffectedByWind = true;
            //affectedByWind = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (affectedByWind)
        {
            Vector3 forceDirection;
            Vector3 ForceMagnitude;
            //If player is still in the trigger, we should just make player go upwards.
            if (!windCameraSpeedOn)
            {
                forceDirection = new Vector3(0,1,0);
            }
            else
            {
                forceDirection = direction;
            }
            float smoothRate = 0.005f;

            rbTarget.AddForce(forceDirection * windMagnitude * distanceBetweenFallingPointAndTrigger, ForceMode.VelocityChange);
            rbTarget.transform.rotation = Quaternion.Lerp(rbTarget.transform.rotation, targetRotation, smoothRate * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (windCameraSpeedOn)
        {
            if(isGrounded()) // when the player touches the ground, give back control
            {
                windCameraSpeedOn = false;
                player.isAffectedByWind = false;
            }

            float followSharpness = 0.0005f;
            Vector3 offSet = Camera.main.transform.position - rbTarget.transform.position;
            float blend = 1f - Mathf.Pow(1f - followSharpness, Time.deltaTime * 30f);

            Camera.main.transform.position = Vector3.Lerp(
                   Camera.main.transform.position,
                   rbTarget.transform.position + offSet,
                   blend);

            Camera.main.transform.rotation = Quaternion.Slerp(
                    Camera.main.transform.rotation,
                    rbTarget.transform.rotation,
                    blend);
            
            if (rbTarget.position.y < transform.position.y) // if the player somehow falls past the wind barrier, reset their position
            {
                rbTarget.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            }
            //if( rbTarget.position.y < 0.0f && rbTarget.position.y < 1.0f) // if player is stuck on the side, push them up
            //{
            //    if(rbTarget.velocity.y < 1.0f)
            //    {
            //        rbTarget.AddForce(Vector3.up * 10);
            //    }
            //}
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(RotateBackAround());
            StartCoroutine(ReEnablePlayerMovementAfterDuration());
            StartCoroutine(CameraFollowPlayerForDuration());
            StartCoroutine(DisableWindAfterDuration());
        }
    }

    private IEnumerator RotateBackAround()
    {
        if(isRotating) // if already rotating, quit
        {
            yield return null;
        }
        float amountRotated = 0f;
        while (amountRotated < rotationAmount)
        {
            float frameRotation = rotationPerSecond * Time.deltaTime;  // Amount to rotate this frame
            rbTarget.transform.Rotate(0, frameRotation, 0);  // Apply the rotation
            amountRotated += frameRotation;  // Also keep track of the amount rotated so far
            yield return new WaitForEndOfFrame();  // We want to rotate every frame until we reach the target
        }
        isRotating = false;
    }

    private IEnumerator CameraFollowPlayerForDuration()
    {
        windCameraSpeedOn = true;
        yield return new WaitForSeconds(durationPlayerLosesControl);
        windCameraSpeedOn = false;
    }

    private IEnumerator ReEnablePlayerMovementAfterDuration()
    {
        yield return new WaitForSeconds(durationPlayerLosesControl);
        if(player) {
            player.isAffectedByWind = false;
        }
    }

    private IEnumerator DisableWindAfterDuration()
    {
        yield return new WaitForSeconds(durationWindAffectsPlayer);
        affectedByWind = false;
    }
    
    private bool isGrounded()
    {
        
        RaycastHit rhInfo;
        return Physics.Raycast(rbTarget.transform.position, Vector3.down, out rhInfo, 1.5f, player.jumpFrom, QueryTriggerInteraction.Ignore);
    }
}
