using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBehaviour : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody r = other.transform.parent.GetComponentInChildren<Rigidbody>();
            float velocity = Mathf.Round(r.velocity.magnitude * 10) / 10;
            if (velocity == 0)
            {
                print("Bravo c'est fini !");
            }
        }
    }
}
