using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private Transform car;
    [SerializeField] private Transform target;
    [SerializeField] private float maxSpeed;


    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = transform.forward * maxSpeed;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            car.position = transform.position;
            car.rotation = transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            car.position = transform.position;
            return;
        }

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

        float steer = 0;
        if (rayCasted > 0)
        {
            float percentageHit = rayHit / (float)rayCasted;
            if (percentageHit > 0.5f)
            {
                var angle = Vector3.SignedAngle(car.forward, vectorToTarget, Vector3.up);
                if (angle > 0) //right
                {
                    steer = 1;
                }
                else if (angle < 0) //left
                {
                    steer = -1;
                }
            }
        }
        car.Rotate(Vector3.up * steer);
        float force = 1f;
        if (steer != 0)
            force = 5;
        rigidBody.AddForce(car.forward * force);
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }
        car.position = transform.position;
    }
    public void SetTarget(Transform t)
    {
        target = t;
    }
}
