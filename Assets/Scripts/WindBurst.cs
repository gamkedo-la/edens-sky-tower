using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBurst : MonoBehaviour
{
    [SerializeField] private float windBurstPowerUp = 100.0f;
    [SerializeField] private float windBurstPowerBack = 250.0f;
    [SerializeField] private float durationPlayerLosesControl = 5.0f;
    [SerializeField] private Transform originTransform;

    private GameObject target;
    private Rigidbody rbTarget;
    private Player player;
    private Vector3 windForce;
    private Vector3 windForceUp;
    private Vector3 windForceBack;
    private Vector3 directionVector;

    private void ApplyWindUp()
    {
        rbTarget.AddForce(windForceUp * windBurstPowerUp, ForceMode.VelocityChange);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ApplyWindUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            rbTarget = other.GetComponent<Rigidbody>();
            player = other.GetComponent<Player>();

            directionVector = (originTransform.position - other.transform.position);
            directionVector = directionVector.normalized;
            windForce = directionVector;

            windForceUp = new Vector3(0.0f, windForce.y, 0.0f);
            windForceBack = new Vector3(windForce.x, 0.0f, windForce.z);

            player.isAffectedByWind = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(ApplyWindBack());
        StartCoroutine(ReEnableMovementInDuration());
    }

    private IEnumerator ApplyWindBack()
    {
        float timePassed = 0;
        while (timePassed < 0.15f)
        {
            rbTarget.AddForce(windForceBack * windBurstPowerBack, ForceMode.VelocityChange);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ReEnableMovementInDuration()
    {
        yield return new WaitForSeconds(durationPlayerLosesControl);
        player.isAffectedByWind = false;
    }
}
