using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostBehaviour : MonoBehaviour
{
    [SerializeField] float boostForce = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().Boost(transform.forward, boostForce);
        }
    }
}
