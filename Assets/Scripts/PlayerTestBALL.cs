using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestBALL : MonoBehaviour
{
    [SerializeField] Transform car;
    [SerializeField] float maxSpeed;

    Rigidbody rig;
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        radius = GetComponent<SphereCollider>().radius;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            car.position = transform.position;
            car.rotation = transform.rotation;
        }

        //Debug.DrawRay(car.position, Vector3.down * 5, Color.red);

        //if (Physics.Raycast(car.position, Vector3.down, out RaycastHit hit, 5f))
        //{
        //    Physics.Raycast(car.position + car.forward, Vector3.down, out RaycastHit forwardHit, 8);
        //    Physics.Raycast(car.position - car.forward, Vector3.down, out RaycastHit backHit);

        //    //Physics.Raycast(car.position + car.right, Vector3.down, out RaycastHit rightHit);
        //    //Physics.Raycast(car.position - car.right, Vector3.down, out RaycastHit leftHit);

        //    Gizmos.DrawSphere(backHit.point, 0.2f);
        //    Gizmos.DrawSphere(forwardHit.point, 0.2f);
        //    //Gizmos.DrawSphere(rightHit.point, 0.2f);
        //    //Gizmos.DrawSphere(leftHit.point, 0.2f);

        //    //Debug.DrawLine(backHit.point + Vector3.down, backHit.point + Vector3.up, Color.blue);
        //    //Debug.DrawLine(forwardHit.point + Vector3.down, forwardHit.point + Vector3.up, Color.blue);
        //    //Debug.DrawLine(rightHit.point + Vector3.down, rightHit.point + Vector3.up, Color.blue);
        //    //Debug.DrawLine(leftHit.point + Vector3.down, leftHit.point + Vector3.up, Color.blue);

        //    float angleFrontBack = Vector3.SignedAngle(car.forward, forwardHit.point - backHit.point, car.right);
        //    //float angleLeftRight = Vector3.SignedAngle(car.right, rightHit.point - leftHit.point, car.forward);
        //    //angleLeftRight = car.rotation.eulerAngles.z;

        //    //car.rotation = Quaternion.Euler(new Vector3(angleFrontBack, car.rotation.eulerAngles.y, angleLeftRight));
        //    car.rotation = Quaternion.Euler(angleFrontBack, car.rotation.eulerAngles.y, car.rotation.eulerAngles.z);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = false;
        if (Physics.Raycast(transform.position, Vector3.down, radius))
            grounded = true;


        float steer = GameManager.Instance.InputController.SteerInput;
        float throttle = GameManager.Instance.InputController.ThrottleInput;

        float force = 5f;

        float angle = Vector3.Angle(car.forward, rig.velocity);
        if (angle < 45 || !grounded)
        {
            car.Rotate(Vector3.up * steer * 1.25f);
        }
        if (steer != 0)
        {
            force *= 2;
        }


        if (false && throttle == 0 && (rig.velocity - Vector3.up * rig.velocity.y).magnitude != 0)
        {
            rig.AddForce(- rig.velocity, ForceMode.Force);
        }
        else
        {
            rig.AddForce(car.forward * throttle * force, ForceMode.Acceleration);

            if (rig.velocity.magnitude > maxSpeed)
            {
                rig.velocity = rig.velocity.normalized * maxSpeed;
            }
        }

        Debug.DrawRay(car.position, car.forward * 10, Color.red);
        Debug.DrawRay(transform.position, rig.velocity.normalized * 10, Color.green);

        Physics.Raycast(car.position, Vector3.down, out RaycastHit hitCenter);
        Vector3 proj = car.forward - (Vector3.Dot(car.forward, hitCenter.normal)) * hitCenter.normal;
        Quaternion rotationRef = Quaternion.Lerp(car.rotation, Quaternion.LookRotation(proj, hitCenter.normal), Time.deltaTime * 10);
        car.rotation = rotationRef;

        car.position = transform.position + Vector3.down * (radius - 0.1f);
    }
}
