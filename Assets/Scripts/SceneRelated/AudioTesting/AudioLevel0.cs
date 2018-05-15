using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLevel0 : MonoBehaviour {
    
    public AudioManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            manager.Play("1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            manager.Loop("1", false);
            while (manager.IsPlaying("1"))
            {

            }
            manager.Loop("1", true);
            manager.Play("1");
            manager.Play("2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            manager.Loop("1", false);
            while (manager.IsPlaying("1"))
            {

            }
            manager.Loop("1", true);
            manager.Play("1");
            manager.Play("3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            manager.Loop("1", false);
            while (manager.IsPlaying("1"))
            {

            }
            manager.Loop("1", true);
            manager.Play("1");
            manager.Play("4");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            manager.Loop("1", false);
            while (manager.IsPlaying("1"))
            {

            }
            manager.Loop("1", true);
            manager.Play("1");
            manager.Play("5");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            manager.Loop("1", false);
            while (manager.IsPlaying("1"))
            {

            }
            manager.Loop("1", true);
            manager.Play("1");
            manager.Play("6");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            manager.Loop("1", false);
            while (manager.IsPlaying("1"))
            {

            }
            manager.Loop("1", true);
            manager.Play("1");
            manager.Play("7");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            manager.Loop("1", false);
            while (manager.IsPlaying("1"))
            {

            }
            manager.Loop("1", true);
            manager.Play("1");
            manager.Play("8");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            manager.Loop("1", false);
            manager.Loop("2", false);
            manager.Loop("3", false);
            manager.Loop("4", false);
            manager.Loop("5", false);
            manager.Loop("6", false);
            manager.Loop("7", false);
            manager.Loop("8", false);
            while (manager.IsPlaying("1"))
            {

            }
            manager.Play("Complete");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            manager.Loop("Complete", false);
            while (manager.IsPlaying("Complete"))
            {

            }
            manager.Play("Ending");
        }
    }
}
