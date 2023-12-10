using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTestBALL : MonoBehaviour
{
    [SerializeField] Transform car;
    [SerializeField] float maxSpeed, force;
    [SerializeField] float maxFuel = 100;
    [SerializeField] float fuelConsumption, fuelQuantity;
    [SerializeField] private Slider fuelGauge;
    [SerializeField, Range(50f, 150f)] float steerRatio = 100f; 

    Rigidbody rig;
    float radius;
    bool boosting = false;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        radius = GetComponent<SphereCollider>().radius;
        fuelQuantity = maxFuel;
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
        if (dead)
        {
            car.position = transform.position + Vector3.down * (radius - 0.1f);
            return;
        }

        bool grounded = false;
        if (Physics.Raycast(transform.position, Vector3.down, 2))
            grounded = true;


        float steer = GameManager.Instance.InputController.SteerInput;
        float throttle = GameManager.Instance.InputController.ThrottleInput;

        if (throttle != 0)
        {
            fuelQuantity -= fuelConsumption * Time.deltaTime;
            fuelGauge.value = fuelQuantity / maxFuel;
        }

        float angle = Vector3.Angle(car.forward, rig.velocity);
        if (true || angle < 45 || !grounded)
        {
            car.Rotate(Vector3.up * steer * steerRatio * Time.deltaTime);
        }
        //if (steer != 0)
        //{
        //    force *= 2;
        //}


        if (boosting && rig.velocity.magnitude > maxSpeed)
        {
            rig.AddForce(-rig.velocity / 5);
        }
        else
        {
            boosting = false;
            if (grounded){
                rig.AddForce(car.forward * throttle * force, ForceMode.Acceleration);
                rig.velocity = Vector3.Project(rig.velocity,car.forward * Time.deltaTime *100 + rig.velocity);
            }

            if (rig.velocity.magnitude > maxSpeed && grounded)
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

    public void Refill()
    {
        fuelQuantity = maxFuel;
        fuelGauge.value = fuelQuantity / maxFuel;
    }

    public void TakeHit()
    {
        if (!dead)
        {
            dead = true;
            Invoke("Die", 1.5f);
        }
    }

    public void Die()
    {
        GameManager.Instance.PlayerDestroyed();
        dead = false;
    }

    public void Shield()
    {
    }

    public void Boost(Vector3 direction)
    {
        boosting = true;
        rig.AddForce(direction * 100, ForceMode.Impulse);
    }
}
