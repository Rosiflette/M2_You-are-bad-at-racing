using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTestBALL : MonoBehaviour
{
    [SerializeField] Transform car;
    [SerializeField] float maxSpeed, force, breakForce;
    [SerializeField] float maxFuel = 100;
    [SerializeField] float fuelConsumption, fuelQuantity;
    [SerializeField] private Slider fuelGauge;
    [SerializeField, Range(50f, 150f)] float steerRatio = 100f;
    [SerializeField] Color startTrailColor, endTrailColor;

    Rigidbody rig;
    TrailRenderer trail;
    float radius;
    bool boosting = false;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        radius = GetComponent<SphereCollider>().radius;
        fuelQuantity = maxFuel;

        trail = gameObject.AddComponent<TrailRenderer>();
        trail.time = 0.2f;
        trail.startColor = startTrailColor;
        trail.endColor = endTrailColor;
        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startWidth = 0.8f;
        trail.endWidth = 0f;
        trail.enabled = true;
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

        if (fuelQuantity < 0)
        {
            TakeHit();
        }

        bool grounded = false;
        if (Physics.Raycast(transform.position, Vector3.down, 2))
            grounded = true;


        float steer = GameManager.Instance.InputController.SteerInput;
        float throttle = GameManager.Instance.InputController.ThrottleInput;

        if (throttle < 0)
        {
            throttle = 0;
        }
        else if (throttle != 0)
        {
            fuelQuantity -= fuelConsumption * Time.deltaTime;
            fuelGauge.value = fuelQuantity / maxFuel;
        }

        float angle = Vector3.Angle(car.forward, rig.velocity);
        if (true || angle < 45 || !grounded)
        {
            car.Rotate(Vector3.up * steer * steerRatio * Time.deltaTime);
        }


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

        if (Input.GetKey(KeyCode.Space))
        {
            rig.angularVelocity = Vector3.zero;
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
            Invoke("Die", 1f);
        }
    }

    public void Die()
    {
        GameManager.Instance.PlayerDestroyed();
        dead = false;
        Refill();
    }

    public void Boost(Vector3 direction, float force)
    {
        boosting = true;
        rig.velocity = rig.velocity.normalized * (maxSpeed + 0.5f);
        rig.AddForce(direction * force, ForceMode.Impulse);
    }
}
