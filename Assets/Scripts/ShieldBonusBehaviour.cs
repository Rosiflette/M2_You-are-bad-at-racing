using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonusBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().Shield();
            Destroy(gameObject);
        }
    }
}
