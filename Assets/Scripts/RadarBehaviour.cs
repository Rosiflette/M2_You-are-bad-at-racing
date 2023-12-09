using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarBehaviour : MonoBehaviour
{
    [SerializeField] GameObject policePrefab;
    [SerializeField] Transform spawnPoint;

    private Transform target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var spawned = Instantiate(policePrefab, spawnPoint.position, spawnPoint.rotation);
            spawned.GetComponentInChildren<AIBehaviour>().SetTarget(target);
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

}
