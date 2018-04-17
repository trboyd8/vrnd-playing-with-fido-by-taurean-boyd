using UnityEngine;

public class PauseMenuManager : MonoBehaviour {

    public static bool IsGamePaused = false;
    public GameObject PauseMenu;
    public GameObject Player;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        IsGamePaused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Pause()
    {
        IsGamePaused = true;
        Time.timeScale = 0f;
        PauseMenu.transform.position = new Vector3(Player.transform.position.x + Player.transform.forward.x * 1.5f, Player.transform.position.y, Player.transform.position.z + Player.transform.forward.z * 1.5f);
        PauseMenu.transform.LookAt(Player.transform.position);
        PauseMenu.transform.Rotate(new Vector3(0, 180, 0));
        PauseMenu.SetActive(true);
    }
}
