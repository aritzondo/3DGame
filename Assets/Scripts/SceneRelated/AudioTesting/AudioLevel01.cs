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
        //manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, true);
        //manager.Play(AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE);
    }

    // Update is called once per frame
    void Update()
    {
        PlayNew(KeyCode.Alpha1, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_11, AudioManager.SoundLevel1.SOUND_LEVEL1_12, ref play11, ref play12);
        PlayNew(KeyCode.Alpha2, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_21, AudioManager.SoundLevel1.SOUND_LEVEL1_22, ref play21, ref play22);
        PlayNew(KeyCode.Alpha3, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_31, AudioManager.SoundLevel1.SOUND_LEVEL1_32, ref play31, ref play32);
        PlayNew(KeyCode.Alpha4, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_41, AudioManager.SoundLevel1.SOUND_LEVEL1_42, ref play41, ref play42);
        PlayNew(KeyCode.Alpha5, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_51, AudioManager.SoundLevel1.SOUND_LEVEL1_52, ref play51, ref play52);
        PlayNew(KeyCode.Alpha6, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_61, AudioManager.SoundLevel1.SOUND_LEVEL1_62, ref play61, ref play62);
        PlayNew(KeyCode.Alpha7, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_71, AudioManager.SoundLevel1.SOUND_LEVEL1_72, ref play71, ref play72);

        PlayGeneral(KeyCode.Alpha8, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_COMPLETE1, AudioManager.SoundLevel1.SOUND_LEVEL1_COMPLETE2, ref playGeneral1, ref playGeneral2);

        PlayEnding(KeyCode.Alpha9, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_ENDING, ref playEnding);
    }

    void PlayNew(KeyCode code, AudioManager.SoundLevel1 reference, AudioManager.SoundLevel1 newSound1, AudioManager.SoundLevel1 newSound2, ref bool boolean1, ref bool boolean2)
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

    void PlayGeneral(KeyCode code, AudioManager.SoundLevel1 reference, AudioManager.SoundLevel1 newSound1, AudioManager.SoundLevel1 newSound2, ref bool boolean1, ref bool boolean2)
    {
        if (Input.GetKeyDown(code))
        {
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_11, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_21, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_31, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_41, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_51, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_61, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_71, false);

            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_12, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_22, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_32, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_42, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_52, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_62, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_72, false);

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

    void PlayEnding(KeyCode code, AudioManager.SoundLevel1 reference, AudioManager.SoundLevel1 newSound, ref bool boolean)
    {
        if (Input.GetKeyDown(code))
        {
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_COMPLETE1, false);
            manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_COMPLETE2, false);

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
