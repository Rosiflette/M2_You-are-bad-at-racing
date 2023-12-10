using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;


public class Level_UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private Sprite pauseImage;
    [SerializeField] private Sprite playImage;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject buttonPause;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject finishMenu;
    [SerializeField] private TextMeshProUGUI textGameFinished;
    [SerializeField] private TextMeshProUGUI textRespawn;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        HUD.SetActive(true);
        finishMenu.SetActive(false);
        textRespawn.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float timePassed = GameManager.Instance.TimePassed;
        int minutes = Mathf.FloorToInt(timePassed / 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);
        int milliseconds = Mathf.FloorToInt((timePassed - Mathf.FloorToInt(timePassed)) * 100);
        textTimer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

    }

    public void RespawningText(float time){
        textRespawn.enabled = true;
        textRespawn.text = "You are respawning in " + time + " seconds";
    }

    public void RespawningTextToFalse(){
        textRespawn.enabled = false;
    }




    public void PauseGame()
    {
        if(Time.timeScale == 0){
            Time.timeScale = 1;
            buttonPause.GetComponent<Image>().sprite = pauseImage;
            pauseMenu.SetActive(false);
        }
        else{
            buttonPause.GetComponent<Image>().sprite = playImage;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void FinishGame(){
        float timePassed = GameManager.Instance.TimePassed;
        int minutes = Mathf.FloorToInt(timePassed / 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);
        int milliseconds = Mathf.FloorToInt((timePassed - Mathf.FloorToInt(timePassed)) * 100);
        string time = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        textGameFinished.text = "You win in " + time;
        HUD.SetActive(false);
        finishMenu.SetActive(true);
    }

    public void LaunchMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
