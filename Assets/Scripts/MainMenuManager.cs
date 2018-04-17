using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public GameObject MainScreen;
    public GameObject InstructionsScreen;
    public GameObject ControlsScreen;

    public void Play()
    {
        // '1' is the index of the game scene
        SceneManager.LoadScene(1);
    }

    public void Instructions()
    {
        MainScreen.SetActive(false);
        InstructionsScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        MainScreen.SetActive(true);
        ControlsScreen.SetActive(false);
    }

    public void Controls()
    {
        InstructionsScreen.SetActive(false);
        ControlsScreen.SetActive(true);
    }
}
