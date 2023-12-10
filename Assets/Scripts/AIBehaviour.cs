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
        rigidBody.velocity = Vector3.Project(rigidBody.velocity,car.forward * Time.deltaTime *100 + rigidBody.velocity);
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

    private void OnTriggerEnter(Collider other)
    {
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
