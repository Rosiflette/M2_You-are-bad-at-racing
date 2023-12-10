using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // if player collide with checkpoint, set respawn point
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.SetRespawn(transform.position, transform.rotation);
        }
    }
}
