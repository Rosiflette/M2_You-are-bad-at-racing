using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;


public class Level_UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimer;

    [SerializeField] private Sprite pauseImage;
    [SerializeField] private Sprite playImage;

    private float timePassed = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timePassed / 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);
        int milliseconds = Mathf.FloorToInt((timePassed - Mathf.FloorToInt(timePassed)) * 100);
        textTimer.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void PauseGame(GameObject button)
    {
        if(Time.timeScale == 0){
            Time.timeScale = 1;
            button.GetComponent<Image>().sprite = pauseImage;
        }
        else{
            button.GetComponent<Image>().sprite = playImage;
            Time.timeScale = 0;
        }
    }
}
