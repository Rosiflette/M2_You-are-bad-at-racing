using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestBALL : MonoBehaviour
{
    [SerializeField] Transform car;

    Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float steer = GameManager.Instance.InputController.SteerInput;
        float throttle = GameManager.Instance.InputController.ThrottleInput;

        rig.AddForce(car.forward * throttle * 5);
        car.position = transform.position;
    }
}
