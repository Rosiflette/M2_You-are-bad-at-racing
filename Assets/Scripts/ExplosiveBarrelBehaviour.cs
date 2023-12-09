using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelBehaviour : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().TakeHit();
            other.transform.parent.GetComponentInChildren<Rigidbody>().AddForce(((other.transform.position + Vector3.up / 2) - transform.position).normalized * 40, ForceMode.Impulse);
            Destroy(gameObject);
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
