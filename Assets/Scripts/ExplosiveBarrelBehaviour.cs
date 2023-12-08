using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrelBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent.GetComponentInChildren<PlayerTestBALL>().TakeHit();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
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
