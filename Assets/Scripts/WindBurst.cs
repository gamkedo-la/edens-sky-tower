using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBurst : MonoBehaviour
{
    [SerializeField] private float windBurstPower = 250.0f;

    private void ApplyWind(GameObject target)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        Vector3 windForce = new Vector3(0.0f, 1.0f, 0.0f) * windBurstPower;
        rb.AddForceAtPosition(windForce, target.transform.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ApplyWind(other.gameObject);
        }
    }
}
