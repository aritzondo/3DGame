using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //you can use it in other scripts
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject keyboardTutorial;
    public GameObject crosshair;

    private bool isGameFinished;
    private float timeToChangeBg;

    public bool GameOver
    {
        get { return isGameFinished; }
        set { isGameFinished = value; }
    }

    private void Start()
    {
        timeToChangeBg = 5.0f;
        isPaused = false;
    }

    void Update ()
    {
        KeyboardTutorialControl();

        if(isGameFinished)
        {
            GetComponentInChildren<Animator>().SetTrigger("GameEnd");
            
            if(Input.anyKey || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Application.Quit();
            }
        }

		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
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
    
    private void KeyboardTutorialControl()
    {

        if (!Input.anyKeyDown && !Input.anyKey && !isPaused && Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            timeToChangeBg -= Time.deltaTime;

            if (timeToChangeBg <= 0.0f)
            {
                keyboardTutorial.SetActive(true);
                crosshair.SetActive(false);
            }
        }
        
        else
        {
            timeToChangeBg = 5.0f;
            keyboardTutorial.SetActive(false);
            crosshair.SetActive(true);

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void GPause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
