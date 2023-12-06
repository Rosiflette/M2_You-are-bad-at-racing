using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] bool steer, invertSteer, power;
    public float SteerAngle, Torque;

    public WheelCollider wheelCollider;

    // Start is called before the first frame update
    void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

    private void FixedUpdate()
    {
        if (steer)
        {
            wheelCollider.steerAngle = SteerAngle * (invertSteer ? -1 : 1);
        }
        if (power)
        {
            wheelCollider.motorTorque = Torque;
        }
    }
}
