using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPaused = true;
            }
            else
            {
                pauseMenu.SetActive(false);
                settingsMenu.SetActive(false);
                controlsMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isPaused = false;
            }
        }
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);

        settingsMenu.SetActive(true);
    }
    public void Controls()
    {
        pauseMenu.SetActive(false);

        controlsMenu.SetActive(true);
    }

    public void Back()
    {
        controlsMenu.SetActive(false);

        settingsMenu.SetActive(false);

        pauseMenu.SetActive(true);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }

}
