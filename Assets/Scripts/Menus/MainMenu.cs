using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_Luis");
    }

    public void Quit()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
	
}
