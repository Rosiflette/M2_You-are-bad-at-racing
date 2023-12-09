using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireBounceBehaviour : MonoBehaviour
{
    [SerializeField, Range(10, 30)] private int bounceForce;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRigidbody = other.transform.parent.GetComponentInChildren<Rigidbody>();
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
        Vector3 bounceDirection = (playerRigidbody.transform.position - transform.position).normalized;
        //float playerSpeed = playerRigidbody.velocity.magnitude;
        playerRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
    }
}
