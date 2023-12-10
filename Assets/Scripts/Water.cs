using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // the water kills anything it collides with
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.GetComponentInChildren<PlayerTestBALL>().Die();
        }
        else if(other.tag == "AI"){
            Destroy(other.transform.parent.gameObject);
        }
        else if (other.tag == "PNJ")
        {
            Destroy(other.gameObject);
        }
    }
}
