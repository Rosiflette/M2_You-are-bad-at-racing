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
        //GetComponent<MeshRenderer>().material.color = Color.red;

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
        Debug.DrawLine(transform.position, target.position, Color.blue);

        Vector3 vectorToTarget = target.position - transform.position;
        Vector3 vectorToTargetNormalized = vectorToTarget.normalized;

        int rayHit = 0;
        int rayCasted = 0;
        for (int i = 1; (i * vectorToTargetNormalized).magnitude < vectorToTarget.magnitude - 1; i++)
        {
            rayCasted++;
            Vector3 posOnLine = transform.position + i * vectorToTargetNormalized;
            
            if (Physics.Raycast(posOnLine, Vector3.down))
            {
                Debug.DrawRay(posOnLine, Vector3.down, Color.green);
                rayHit++;
            }
            else
                Debug.DrawRay(posOnLine, Vector3.down, Color.red);
        }

        if (rayCasted > 0)
        {
            //Debug.Log($"{rayHit} hits on {rayCasted} raycast : {(rayHit / (float)rayCasted) * 100}%");

            float percentageHit = rayHit / (float)rayCasted;
            if (percentageHit > 0.85f)
            {
                float rotationAngle = rotationSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, (target.position - transform.position).normalized, rotationAngle, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }

        //float horizontal = Input.GetAxis("Horizontal");

        
        //Quaternion rotation = Quaternion.Euler(0f, rotationAngle, 0f);

        

        Vector3 nextPos = Vector3.forward.normalized * maxSpeed;

        velocity = Vector3.Lerp(velocity, nextPos, Time.deltaTime * acceleration);

        ////gameObject.transform.rotation *= rotation;
        transform.Translate(velocity * Time.deltaTime);

    }
}
