using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Despite the name, this is the FINAL
 * player controller
 * (to scared to rename it before the "rendu" :( )
 */

public class PlayerTestBALL : MonoBehaviour
{
    [SerializeField] Transform car;
    [SerializeField] float maxSpeed, force, brakeForce;
    [SerializeField] float maxFuel = 100;
    [SerializeField] float fuelConsumption, fuelQuantity;
    [SerializeField] private Slider fuelGauge;
    [SerializeField, Range(50f, 150f)] float steerRatio = 100f;
    [SerializeField] Color startTrailColor, endTrailColor;
    [SerializeField] Material mat;

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

        // create a trail behind the player (it's pretty)
        trail = gameObject.AddComponent<TrailRenderer>();
        trail.time = 0.2f;
        trail.startColor = startTrailColor;
        trail.endColor = endTrailColor;
        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startWidth = 0.8f;
        trail.endWidth = 0f;
        trail.enabled = true;
        mat.SetFloat("_burnFactor", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            // place the mesh at the location of the collider
            car.position = transform.position + Vector3.down * (radius - 0.1f);
            return;
        }

        // if the player fuel gauge hits 0
        // kill the player
        if (fuelQuantity < 0)
        {
            TakeHit();
        }

        bool grounded = false;
        if (Physics.Raycast(transform.position, Vector3.down, 2))
            grounded = true;


        float steer = GameManager.Instance.InputController.SteerInput;
        float throttle = GameManager.Instance.InputController.ThrottleInput;

        if (throttle < 0) // disable backward movement
        {
            throttle = 0;
        }
        else if (throttle != 0) // reduce fuel gauge
        {
            fuelQuantity -= fuelConsumption * Time.deltaTime;
            fuelGauge.value = fuelQuantity / maxFuel;
        }

        car.Rotate(Vector3.up * steer * steerRatio * Time.deltaTime);

        if (boosting && rig.velocity.magnitude > maxSpeed) // allows the player to be faster than his max speed
        {
            //gradually decrease player speed until it reaches maxSpeed
            rig.AddForce(-rig.velocity / 5);
        }
        else
        {
            boosting = false;
            if (grounded)
            {
                rig.AddForce(car.forward * throttle * force, ForceMode.Acceleration);
                // project rigibody velocity on the forward vector of the mesh
                // allows sharper turn
                rig.velocity = Vector3.Project(rig.velocity, car.forward * Time.deltaTime * 100 + rig.velocity);
            }

            if (rig.velocity.magnitude > maxSpeed && grounded)
            {
                rig.velocity = rig.velocity.normalized * maxSpeed;
            }
        }

        if (Input.GetKey(KeyCode.Space)) // brake
        {
            rig.angularVelocity = Vector3.zero;
        }

        Debug.DrawRay(car.position, car.forward * 10, Color.red);
        Debug.DrawRay(transform.position, rig.velocity.normalized * 10, Color.green);

        // BEGIN
        // align player rotation with the road shape
        Physics.Raycast(car.position, Vector3.down, out RaycastHit hitCenter);
        Vector3 proj = car.forward - (Vector3.Dot(car.forward, hitCenter.normal)) * hitCenter.normal;
        Quaternion rotationRef = Quaternion.Lerp(car.rotation, Quaternion.LookRotation(proj, hitCenter.normal), Time.deltaTime * 10);
        car.rotation = rotationRef;
        // END

        // place the mesh at the location of the collider
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
            transform.parent.GetComponentInChildren<Level_UIManager>().RespawningText(1f);
            mat.SetFloat("_burnFactor", 0.8f);
        }
    }

    public void Die()
    {
        // notify the GameManger that the player is dead for it to respawn the player
        GameManager.Instance.PlayerDestroyed();
        dead = false;
        Refill();
        mat.SetFloat("_burnFactor", 0);
    }

    public void Boost(Vector3 direction, float force)
    {
        // push the player in a direction
        // overriding its maxSpeed
        boosting = true;
        rig.velocity = rig.velocity.normalized * (maxSpeed + 0.5f);
        rig.AddForce(direction * force, ForceMode.Impulse);
    }
}
