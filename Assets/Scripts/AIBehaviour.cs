using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(1, 50)] private float maxSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float acceleration;

    private Vector3 velocity = Vector3.zero;

    TrailRenderer trail;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;

        trail = gameObject.AddComponent<TrailRenderer>();
        trail.time = 5;
        trail.material.color = Color.red;
        trail.startWidth = 0.5f;
        trail.endWidth = 0f;
        trail.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal");

        float rotationAngle = rotationSpeed * Time.deltaTime; 
        //Quaternion rotation = Quaternion.Euler(0f, rotationAngle, 0f);

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, (target.position - transform.position).normalized, rotationAngle, 0.0f);

        Vector3 nextPos = Vector3.forward.normalized * maxSpeed;

        velocity = Vector3.Lerp(velocity, nextPos, Time.deltaTime * acceleration);

        //gameObject.transform.rotation *= rotation;
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.Translate(velocity * Time.deltaTime);

    }
}
