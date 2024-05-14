using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartGame(){
        SceneManager.LoadScene("MapScene");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
