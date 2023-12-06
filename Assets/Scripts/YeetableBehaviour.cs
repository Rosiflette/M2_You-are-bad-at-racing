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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 yeetDirection = (transform.position - collision.transform.position).normalized + Vector3.up;
            yeetDirection = yeetDirection.normalized;

            rigidBody.AddForce(yeetDirection * 5, ForceMode.Impulse);
        }
    }
}
