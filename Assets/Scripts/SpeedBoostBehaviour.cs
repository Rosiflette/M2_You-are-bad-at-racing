using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().Boost(transform.forward);
        }
    }
}
