using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeetableBehaviour : MonoBehaviour
{
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // if the player collides with the object
            // sends the object flying (fun :) )
            Vector3 yeetDirection = (transform.position - other.transform.position).normalized + Vector3.up;
            rigidBody.AddForce(yeetDirection * 10, ForceMode.Impulse);
        }
    }
}
