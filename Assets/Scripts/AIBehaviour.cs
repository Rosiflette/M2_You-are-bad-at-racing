using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField] private Transform car;
    [SerializeField] private Transform target;
    [SerializeField] private float maxSpeed, force;
    [SerializeField] private GameObject explosion;

    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // AI starts with maximum velocity
        rigidBody.velocity = transform.forward * maxSpeed;
    }

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

        // shoot a ray down periodically to check if the collide with the road
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
            // AI needs at least 60% of ray colliding with road to turn to target
            // allows to anticipate turns
            if (percentageHit > 0.6f)
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
        rigidBody.AddForce(car.forward * force);
        
        // project rigibody velocity on the forward vector of the mesh
        // allows sharper turn
        rigidBody.velocity = Vector3.Project(rigidBody.velocity, car.forward * Time.deltaTime * 100 + rigidBody.velocity);
        
        // cap AI speed to maxSpeed
        if (rigidBody.velocity.magnitude > maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.normalized * maxSpeed;
        }

        // place the mesh at the location of the collider
        car.position = transform.position;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    private void OnTriggerEnter(Collider other)
    {
        // AI kills the player when colliding
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().TakeHit();
            Invoke("Die", 1f);
        }
    }

    private void Die()
    {
        Destroy(transform.parent.gameObject);
    }
}
