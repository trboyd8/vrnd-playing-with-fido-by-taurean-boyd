using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public void Play()
    {
        // '1' is the index of the game scene
        SceneManager.LoadScene(1);
    }

    public void Instructions()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
