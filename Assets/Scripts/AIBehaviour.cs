using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private float motorTorque = 1500f;
    [SerializeField] private float maxSteer = 15f;
    [SerializeField] private Color trailColor;
    [SerializeField] private Transform target;

    private TrailRenderer trail;
    private Rigidbody rigidBody;
    private Wheel[] wheels;

    // Start is called before the first frame update
    void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = centerOfMass.localPosition;

        trail = gameObject.AddComponent<TrailRenderer>();
        trail.time = 5;
        trail.material.color = trailColor;
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
            {
                Debug.DrawRay(posOnLine, Vector3.down, Color.red);
            }
        }

        float Steer = 0;
        if (rayCasted > 0)
        {
            float percentageHit = rayHit / (float)rayCasted;
            if (percentageHit > 0.85f)
            {
                var angle = Vector3.SignedAngle(transform.forward, vectorToTarget, Vector3.up);
                if (angle > 0) //right
                {
                    print("RIGHT");
                    Steer = 1;
                }
                else if (angle < 0) //left
                {
                    print("LEFT");
                    Steer = -1;
                }
            }
        }

        foreach (Wheel wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = motorTorque;
        }
    }
}
