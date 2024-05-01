using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuObject, settingsMenuObject, creditsMenuObject;

    public bool mainMenu = true;
    public bool settingsMenu = false;
    public bool credits = false;


    public GameObject firstMainMenuSelection, firstSettingsMenuSelection, settingsClosedSelection, creditsSelection, CreditsClosedSelection;

    private void Start()
    {
        if (mainMenu)
        {
            EventSystem.current.SetSelectedGameObject(firstMainMenuSelection);
        }
        settingsMenuObject.SetActive(false);
        mainMenuObject.SetActive(true);
        creditsMenuObject.SetActive(false);
    }
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Settings()
    {
        settingsMenuObject.SetActive(true);
        mainMenuObject.SetActive(false);
        creditsMenuObject.SetActive(false);
        mainMenu = false;
        settingsMenu = true;
        credits = false;

        if (settingsMenu) 
        { 
            EventSystem.current.SetSelectedGameObject(firstSettingsMenuSelection);
        }
    }
    public void Menu()
    {
        mainMenuObject.SetActive(true);
        settingsMenuObject.SetActive(false);
        creditsMenuObject.SetActive(false);
        mainMenu = true;
        settingsMenu = false;
        credits = false;

        if (mainMenu)
        {
            EventSystem.current.SetSelectedGameObject(settingsClosedSelection);
        }
    }

    public void Credits()
    {
        creditsMenuObject.SetActive(true);
        settingsMenuObject.SetActive(false);
        mainMenuObject.SetActive(false);
        credits = true;
        settingsMenu = false;
        mainMenu = false;

        if(credits)
        {
            EventSystem.current.SetSelectedGameObject(creditsSelection);
        }
    }
}
