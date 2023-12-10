using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public InputController InputController { get; private set; }
    public float TimePassed { get; private set; }

    [SerializeField] private GameObject playerPrefab;

    private Vector3 respawnPosition;
    private Quaternion respawnOrientation;
    private GameObject player = null;
    private GameObject[] radars;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        TimePassed = 0;
        InputController = GetComponentInChildren<InputController>();
        respawnPosition = transform.position;
        respawnOrientation = transform.rotation;

        radars = GameObject.FindGameObjectsWithTag("Radar");

        SpawnPlayer();
    }

    private void Update()
    {
        TimePassed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnPlayer();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
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
            Rigidbody playerRigiBody = player.GetComponentInChildren<Rigidbody>();
            playerRigiBody.velocity = Vector3.zero;
            playerRigiBody.angularVelocity = Vector3.zero;
            playerRigiBody.transform.position = respawnPosition;
            player.GetComponentInChildren<BoxCollider>().transform.rotation = respawnOrientation;
            player.GetComponentInChildren<PlayerTestBALL>().Refill();
        }
    }

    public void PlayerDestroyed()
    {
        SpawnPlayer();
    }

    public void SetRespawn(Vector3 position, Quaternion orientation)
    {
        respawnPosition = position;
        respawnOrientation = orientation;
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
