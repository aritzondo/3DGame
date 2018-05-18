using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLevel01 : MonoBehaviour
{
    #region Public Attributes

    public AudioManager manager;

    #endregion

    #region Private Attributes

    private bool play11 = false;
    private bool play21 = false;
    private bool play31 = false;
    private bool play41 = false;
    private bool play51 = false;
    private bool play61 = false;
    private bool play71 = false;
    private bool playGeneral1 = false;

    private bool play12 = false;
    private bool play22 = false;
    private bool play32 = false;
    private bool play42 = false;
    private bool play52 = false;
    private bool play62 = false;
    private bool play72 = false;
    private bool playGeneral2 = false;

    private bool playEnding = false;

    #endregion

    // Use this for initialization
    void Start()
    {
        manager.Loop("Reference", true);
        manager.Play("Reference");
    }

    // Update is called once per frame
    void Update()
    {
        PlayNew(KeyCode.Alpha1, "Reference", "1-1", "1-2", ref play11, ref play12);
        PlayNew(KeyCode.Alpha2, "Reference", "2-1", "2-2", ref play21, ref play22);
        PlayNew(KeyCode.Alpha3, "Reference", "3-1", "3-2", ref play31, ref play32);
        PlayNew(KeyCode.Alpha4, "Reference", "4-1", "4-2", ref play41, ref play42);
        PlayNew(KeyCode.Alpha5, "Reference", "5-1", "5-2", ref play51, ref play52);
        PlayNew(KeyCode.Alpha6, "Reference", "6-1", "6-2", ref play61, ref play62);
        PlayNew(KeyCode.Alpha7, "Reference", "7-1", "7-2", ref play71, ref play72);

        PlayGeneral(KeyCode.Alpha8, "Reference", "Complete-1", "Complete-2",ref playGeneral1, ref playGeneral2);

        PlayEnding(KeyCode.Alpha9, "Reference", "Ending", ref playEnding);
    }

    void PlayNew(KeyCode code, string reference, string newSound1, string newSound2, ref bool boolean1, ref bool boolean2)
    {
        if (Input.GetKeyDown(code))
        {
            manager.Loop(reference, false);
            boolean1 = true;
        }

        if (!manager.IsPlaying(reference) && boolean1)
        {
            manager.Play(reference);
            manager.Play(newSound1);
            boolean1 = false;
            boolean2 = true;
        }

        if (!manager.IsPlaying(reference) && boolean2)
        {
            manager.Loop(reference, true);
            manager.Play(reference);
            manager.Play(newSound2);
            boolean2 = false;
        }
    }

    void PlayGeneral(KeyCode code, string reference, string newSound1, string newSound2, ref bool boolean1, ref bool boolean2)
    {
        if (Input.GetKeyDown(code))
        {
            manager.Loop("1-1", false);
            manager.Loop("2-1", false);
            manager.Loop("3-1", false);
            manager.Loop("4-1", false);
            manager.Loop("5-1", false);
            manager.Loop("6-1", false);
            manager.Loop("7-1", false);

            manager.Loop("1-2", false);
            manager.Loop("2-2", false);
            manager.Loop("3-2", false);
            manager.Loop("4-2", false);
            manager.Loop("5-2", false);
            manager.Loop("6-2", false);
            manager.Loop("7-2", false);

            manager.Loop(reference, false);
            boolean1 = true;
        }

        if (!manager.IsPlaying(reference) && boolean1)
        {
            manager.Play(reference);
            manager.Play(newSound1);
            boolean1 = false;
            boolean2 = true;
        }

        if (!manager.IsPlaying(reference) && boolean2)
        {
            manager.Loop(reference, true);
            manager.Play(reference);
            manager.Play(newSound2);
            boolean2 = false;
        }
    }

    void PlayEnding(KeyCode code, string reference, string newSound, ref bool boolean)
    {
        if (Input.GetKeyDown(code))
        {
            manager.Loop("Complete-1", false);
            manager.Loop("Complete-2", false);

            manager.Loop(reference, false);
            boolean = true;
        }

        if (!manager.IsPlaying(reference) && boolean)
        {
            //manager.Loop(reference, true);
            //manager.Play(reference);
            manager.Play(newSound);
            boolean = false;
        }
    }
}
