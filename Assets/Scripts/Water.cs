using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().Die();
        }
        else if(other.tag == "AI"){
            Destroy(other.transform.parent.gameObject);
        }
    }
}
