using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireBounceBehaviour : MonoBehaviour
{
    [SerializeField, Range(100, 5000)] private int bounceForce = 500;
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 bounceDirection = (collision.transform.position - transform.position).normalized;
        float playerSpeed = playerRigidbody.velocity.magnitude;
        playerRigidbody.AddForce(bounceDirection * bounceForce * playerSpeed, ForceMode.Impulse);
    }
}
