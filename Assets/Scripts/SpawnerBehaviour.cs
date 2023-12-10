using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn PNJ cars in a semi-randomize time
public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float spawnMinTime, spawnMaxTime;

    Transform limit;
    float spawnTime;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        limit = transform.GetChild(0).transform;
        t = 0;
        spawnTime = Random.Range(spawnMinTime, spawnMaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > spawnTime)
        {
            Vector3 position = transform.position - Random.value * (transform.position - limit.position);
            Instantiate(prefab, position, transform.rotation, transform);
            t = 0;
            spawnTime = Random.Range(spawnMinTime, spawnMaxTime);
        }
    }
}
