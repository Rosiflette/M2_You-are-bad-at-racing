using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 10f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        float rotationAngle = horizontal * rotationSpeed * Time.deltaTime;

        Vector3 inputDirection = new Vector3(0f, 0f, vertical).normalized;
        Quaternion rotation = Quaternion.Euler(0f, rotationAngle, 0f);

        Vector3 nextPos = inputDirection * maxSpeed;

        velocity = Vector3.Lerp(velocity, nextPos, Time.deltaTime * acceleration);
        if (inputDirection == Vector3.zero)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);
        }
        gameObject.transform.rotation *= rotation;
        gameObject.transform.Translate(velocity * Time.deltaTime);

    }

}
