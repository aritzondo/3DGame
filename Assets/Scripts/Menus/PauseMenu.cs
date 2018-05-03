using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //you can use it in other scripts
    public static bool isPaused;
    public GameObject pauseMenuUI;

    private void Start()
    {
        setCameraLock(CursorLockMode.Locked);
        isPaused = false;
    }

    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
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
        togleCameraLock();
    }

    void GPause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
        togleCameraLock();
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

    public void togleCameraLock()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void setCameraLock(CursorLockMode newLock)
    {
        Cursor.lockState = newLock;
        Cursor.visible = newLock == CursorLockMode.None;
    }
}
