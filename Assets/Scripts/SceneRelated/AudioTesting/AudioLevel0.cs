/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLevel0 : MonoBehaviour {
    
    public AudioManager manager;

    private bool play1 = false;
    private bool play2 = false;
    private bool play3 = false;
    private bool play4 = false;
    private bool play5 = false;
    private bool play6 = false;
    private bool play7 = false;
    private bool playGeneral = false;
    private bool playEnding = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            manager.Play("1");
        }

        PlayNew(KeyCode.Alpha2, "1", "2", ref play1);
        PlayNew(KeyCode.Alpha3, "1", "3", ref play2);
        PlayNew(KeyCode.Alpha4, "1", "4", ref play3);
        PlayNew(KeyCode.Alpha5, "1", "5", ref play4);
        PlayNew(KeyCode.Alpha6, "1", "6", ref play5);
        PlayNew(KeyCode.Alpha7, "1", "7", ref play6);
        PlayNew(KeyCode.Alpha8, "1", "8", ref play7);

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            manager.Loop("1", false);
            manager.Loop("2", false);
            manager.Loop("3", false);
            manager.Loop("4", false);
            manager.Loop("5", false);
            manager.Loop("6", false);
            manager.Loop("7", false);
            playGeneral = true;
        }
        if (!manager.IsPlaying("1") && playGeneral)
        {
            manager.Play("Complete");
            playGeneral = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            manager.Loop("Complete", false);
            playEnding = true;
        }
        if (!manager.IsPlaying("Complete") && playEnding)
        {
            manager.Play("Ending");
            playEnding = false;
        }
    }

    void PlayNew (KeyCode code, string oldSound, string newSound, ref bool boolean)
    {
        if (Input.GetKeyDown(code))
        {
            manager.Loop(oldSound, false);
            boolean = true;
        }

        if (!manager.IsPlaying(oldSound) && boolean)
        {
            manager.Loop(oldSound, true);
            manager.Play(oldSound);
            manager.Play(newSound);
            boolean = false;
        }
    }
}
*/