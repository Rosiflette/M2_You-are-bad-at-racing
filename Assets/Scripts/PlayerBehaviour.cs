using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private float motorTorque = 1500f;
    [SerializeField] private float maxSteer = 20f;
    [SerializeField] private Color trailColor;

    public float Steer { get; set; }
    public float Throttle { get; set; }

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
        Steer = GameManager.Instance.InputController.SteerInput;
        Throttle = GameManager.Instance.InputController.ThrottleInput;

        foreach (Wheel wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
        }
    }
}
