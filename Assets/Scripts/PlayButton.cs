using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public GameObject Pannel;
    public GameObject PauseMenuPannel;
    
    //Play Button
    public void Play()
    {
        Time.timeScale = 1.0f;
        Pannel.SetActive(false);
        Debug.Log("Playing");
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseMenuPannel.SetActive(false);
        Debug.Log("Playing");
    }
}
