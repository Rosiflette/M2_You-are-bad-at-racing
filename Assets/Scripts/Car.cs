using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 10f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        Vector3 nextPos = inputDirection * maxSpeed;

        velocity = Vector3.Lerp(velocity, nextPos, Time.deltaTime * acceleration);

        if (inputDirection == Vector3.zero)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);
        }

        transform.Translate(velocity * Time.deltaTime);
    }


}
