using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //you can use it in other scripts
    public static bool isPaused;
    public GameObject pauseMenuUI;

    private bool lockCursor = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.E))
        {
            if (!isPaused)
            {
                lockCursor = !lockCursor;
            }
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (isPaused)
            {
                Resume();
            }
            else
            {
                GPause();
            }
        }

        
    }
    
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;

        lockCursor = !lockCursor;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void GPause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void Menu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main_Menu");
    }

    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
