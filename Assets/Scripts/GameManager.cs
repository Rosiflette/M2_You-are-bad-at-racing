using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public InputController InputController { get; private set; }

    [SerializeField] private GameObject playerPrefab;

    private Vector3 respawnPosition;
    private GameObject player = null;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        InputController = GetComponentInChildren<InputController>();
        respawnPosition = transform.position;

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
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        if (player == null)
        {
            player = Instantiate(playerPrefab, respawnPosition, Quaternion.identity);
        }
        else
        {
            player.GetComponent<PlayerBehaviour>().StopCar();
            player.transform.position = respawnPosition;
        }
    }

    public void SetRespawn(Vector3 position)
    {
        respawnPosition = position;
    }

    public void PlayerDestroyed()
    {
        player = null;
    }
}
