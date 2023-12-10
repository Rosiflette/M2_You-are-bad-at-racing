using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillerBehaviour : MonoBehaviour
{
    [SerializeField, Range(5, 50)] float repulsionForce;
    [SerializeField] GameObject explosion; // explosion FX

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // hit the player
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().TakeHit();
            Rigidbody r = other.transform.parent.GetComponentInChildren<Rigidbody>();
            
            if (gameObject.tag == "Explosive") // for explosive barrel
            {
                // push back the player
                r.AddForce(((other.transform.position + Vector3.up / 2) - transform.position).normalized * repulsionForce, ForceMode.Impulse);
                Destroy(gameObject);
            }
            else
            {
                // stop the player, then push it
                r.velocity = Vector3.zero;
                r.angularVelocity = Vector3.zero;
                r.AddForce(-transform.up, ForceMode.Impulse);
            }
        }
    }

    private void OnDestroy()
    {
        // instantiate explosion FX
        // source of the error : "Some objects were not cleaned up when closin the scene."
        // but the explosion FX destroys itself at the end of the animation
        Instantiate(explosion, transform.position, Quaternion.identity);
        Collider[] hits = Physics.OverlapSphere(transform.position, 3);

        // destroy all surrounding explosives
        // allows chain reaction
        foreach (Collider hit in hits)
        {
            if (hit.tag == "Explosive")
            {
                Destroy(hit.gameObject);
            }
        }
    }
}
