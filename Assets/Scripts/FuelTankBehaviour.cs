using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTankBehaviour : MonoBehaviour
{
    [SerializeField, Range(0f, 8f)] float speed = 2.5f;
    [SerializeField, Range(0f, 90f)] float rotate = 2.5f;
    Vector3 basePosition;

    private void Awake()
    {
        basePosition = transform.position;
    }

    private void Update()
    {
        // idle animation
        transform.position = basePosition + Vector3.up * Mathf.Sin(Time.time * speed) / 2;
        transform.rotation = Quaternion.Euler(Vector3.up * rotate * Time.time);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if player collide with fuel tank, refill the car
        // then disable fuel tank and re-enable it after 3 seconds
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().Refill();
            Invoke("Enable", 3);
            gameObject.SetActive(false);
        }
    }

    private void Enable()
    {
        gameObject.SetActive(true);
    }
}
