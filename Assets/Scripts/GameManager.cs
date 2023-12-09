using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public InputController InputController { get; private set; }

    [SerializeField] private GameObject playerPrefab;

    private Vector3 respawnPosition;
    private Quaternion respawnOrientation;
    private GameObject player = null;
    private bool playerAlive = false;
    private GameObject[] radars;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        InputController = GetComponentInChildren<InputController>();
        respawnPosition = transform.position;
        respawnOrientation = transform.rotation;

        radars = GameObject.FindGameObjectsWithTag("Radar");

        SpawnPlayer();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnPlayer();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            respawnPosition = transform.position;
            respawnOrientation = transform.rotation;
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        if (player == null)
        {
            player = Instantiate(playerPrefab, respawnPosition, respawnOrientation);
            foreach (var radar in radars)
            {
                radar.GetComponent<RadarBehaviour>().SetTarget(player.GetComponentInChildren<SphereCollider>().transform);
            }
        }
        else
        {
            //player.GetComponentInChildren<PlayerBehaviour>().StopCar();
            player.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
            player.GetComponentInChildren<SphereCollider>().transform.position = respawnPosition;
            //player.transform.rotation = respawnOrientation;
        }
    }

    public void PlayerDestroyed()
    {

    }

    public void SetRespawn(Vector3 position, Quaternion orientation)
    {
        respawnPosition = position;
        respawnOrientation = orientation;
    }
}
