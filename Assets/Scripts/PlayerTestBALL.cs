using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestBALL : MonoBehaviour
{
    [SerializeField] Transform car;
    [SerializeField] float maxSpeed;

    Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            car.position = transform.position;
            car.rotation = transform.rotation;
        }

        Debug.DrawRay(transform.position, Vector3.down * 5, Color.red);

        if (Physics.Raycast(car.position, Vector3.down, out RaycastHit hit, 5f))
        {
            Physics.Raycast(car.position + car.forward, Vector3.down, out RaycastHit forwardHit);
            Physics.Raycast(car.position - car.forward, Vector3.down, out RaycastHit backHit);

            Physics.Raycast(car.position + car.right, Vector3.down, out RaycastHit rightHit);
            Physics.Raycast(car.position - car.right, Vector3.down, out RaycastHit leftHit);

            Gizmos.DrawSphere(backHit.point, 0.25f);
            Gizmos.DrawSphere(forwardHit.point, 0.25f);
            Gizmos.DrawSphere(rightHit.point, 0.25f);
            Gizmos.DrawSphere(leftHit.point, 0.25f);

            float angleFrontBack = Vector3.SignedAngle(car.forward, forwardHit.point - backHit.point, car.right);
            float angleLeftRight = Vector3.SignedAngle(car.right, rightHit.point - leftHit.point, car.forward);
            //angleLeftRight = car.rotation.eulerAngles.z;

            car.rotation = Quaternion.Euler(new Vector3(angleFrontBack, car.rotation.eulerAngles.y, angleLeftRight));
        }
    }

    // Update is called once per frame
    void Update()
    {
        float steer = GameManager.Instance.InputController.SteerInput;
        float throttle = GameManager.Instance.InputController.ThrottleInput;

        float force = 10f;

        car.Rotate(Vector3.up * steer * 1.5f);
        if (steer != 0)
        {
            force *= 2;
        }


        if (throttle == 0 && (rig.velocity - Vector3.up * rig.velocity.y).magnitude != 0)
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

        if (Physics.Raycast(car.position, Vector3.down, out RaycastHit hit, 5f))
        {
            Physics.Raycast(car.position + car.forward, Vector3.down, out RaycastHit forwardHit);
            Physics.Raycast(car.position - car.forward, Vector3.down, out RaycastHit backHit);

            Physics.Raycast(car.position + car.right, Vector3.down, out RaycastHit rightHit);
            Physics.Raycast(car.position - car.right, Vector3.down, out RaycastHit leftHit);

            //Gizmos.DrawSphere(backHit.point, 0.25f);
            //Gizmos.DrawSphere(forwardHit.point, 0.25f);
            //Gizmos.DrawSphere(rightHit.point, 0.25f);
            //Gizmos.DrawSphere(leftHit.point, 0.25f);

            //float heightDiff = forwardHit.point.y - backHit.point.y;

            float angleFrontBack = Vector3.SignedAngle(car.forward, forwardHit.point - backHit.point, car.right);
            float angleLeftRight = Vector3.SignedAngle(car.right, rightHit.point - leftHit.point, car.forward);
            angleLeftRight = car.rotation.eulerAngles.z;

            car.rotation = Quaternion.Euler(new Vector3(angleFrontBack, car.rotation.eulerAngles.y, angleLeftRight));
        }

        car.position = transform.position;
    }
}
