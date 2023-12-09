using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillerBehaviour : MonoBehaviour
{
    [SerializeField, Range(5, 50)] float repulsionForce;
    [SerializeField] GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().TakeHit();
            Rigidbody r = other.transform.parent.GetComponentInChildren<Rigidbody>();
            if (gameObject.tag == "Explosive")
            {
                r.AddForce(((other.transform.position + Vector3.up / 2) - transform.position).normalized * repulsionForce, ForceMode.Impulse);
                Destroy(gameObject);
            }
            else
            {
                
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                r.AddForce(-transform.up, ForceMode.Impulse);
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Collider[] hits = Physics.OverlapSphere(transform.position, 3);

        foreach (Collider hit in hits)
        {
            if (hit.tag == "Explosive")
            {
                Destroy(hit.gameObject);
            }
        }
    }
}
