using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    private void Start()
    {
        // randomize car speed
        speed += Random.value * 20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // push the player
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().Boost(transform.forward, speed / 2);
        }
    }
}
