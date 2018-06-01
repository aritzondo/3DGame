using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelsMusicManager : MonoBehaviour
{
    #region Public Attributes


    [HideInInspector]
    public bool level1Finished = false;
    [HideInInspector]
    public bool level2Finished = false;
    [HideInInspector]
    public bool level3Finished = false;
    [HideInInspector]
    public bool level4Finished = false;
    [HideInInspector]
    public bool level5Finished = false;
    [HideInInspector]
    public bool level6Finished = false;
    [HideInInspector]
    public bool level7Finished = false;
    [HideInInspector]
    public bool level8Finished = false;
    [HideInInspector]
    public bool level9Finished = false;

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
    private AudioManager manager;

    #endregion

    // Use this for initialization
    void Start()
    {
        if (manager == null)
        {
            manager = AudioManager.GetInstance();
        }
        //manager.Loop(AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, true);
        //manager.Play(AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE);
    }

    // Update is called once per frame
    void Update()
    {
        PlayNew(level1Finished, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_11, AudioManager.SoundLevel1.SOUND_LEVEL1_12, ref play11, ref play12);
        PlayNew(level2Finished, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_21, AudioManager.SoundLevel1.SOUND_LEVEL1_22, ref play21, ref play22);
        PlayNew(level3Finished, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_31, AudioManager.SoundLevel1.SOUND_LEVEL1_32, ref play31, ref play32);
        PlayNew(level4Finished, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_41, AudioManager.SoundLevel1.SOUND_LEVEL1_42, ref play41, ref play42);
        PlayNew(level5Finished, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_51, AudioManager.SoundLevel1.SOUND_LEVEL1_52, ref play51, ref play52);
        PlayNew(level6Finished, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_61, AudioManager.SoundLevel1.SOUND_LEVEL1_62, ref play61, ref play62);
        PlayNew(level7Finished, AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_71, AudioManager.SoundLevel1.SOUND_LEVEL1_72, ref play71, ref play72);

        PlayGeneral(AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_COMPLETE1, AudioManager.SoundLevel1.SOUND_LEVEL1_COMPLETE2, ref playGeneral1, ref playGeneral2);

        PlayEnding(AudioManager.SoundLevel1.SOUND_LEVEL1_REFERENCE, AudioManager.SoundLevel1.SOUND_LEVEL1_ENDING, ref playEnding);
    }

    public void levelFinished(int levelsCompleted)
    {
        switch (levelsCompleted)
        {
            case 1:
                level1Finished = true;
                break;
            case 2:
                level2Finished = true;
                break;
            case 3:
                level3Finished = true;
                break;
            case 4:
                level4Finished = true;
                break;
            case 5:
                level5Finished = true;
                break;
            case 6:
                level6Finished = true;
                break;
            case 7:
                level7Finished = true;
                break;
            case 8:
                level8Finished = true;
                break;
            case 9:
                level9Finished = true;
                break;
            default:
                break;
        }
    }

    void PlayNew(bool code, AudioManager.SoundLevel1 reference, AudioManager.SoundLevel1 newSound1, AudioManager.SoundLevel1 newSound2, ref bool boolean1, ref bool boolean2)
    {
        if (code)
        {
            manager.Loop((int)reference, false);
            boolean1 = true;
            code = false;
        }

        if (!manager.IsPlaying((int)reference) && boolean1)
        {
            manager.Play((int)reference);
            manager.Play((int)newSound1);
            boolean1 = false;
            boolean2 = true;
        }

        if (!manager.IsPlaying((int)reference) && boolean2)
        {
            manager.Loop((int)reference, true);
            manager.Play((int)reference);
            manager.Play((int)newSound2);
            boolean2 = false;
        }
    }

    void PlayGeneral(AudioManager.SoundLevel1 reference, AudioManager.SoundLevel1 newSound1, AudioManager.SoundLevel1 newSound2, ref bool boolean1, ref bool boolean2)
    {
        if (level8Finished)
        {
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_11, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_21, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_31, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_41, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_51, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_61, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_71, false);

            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_12, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_22, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_32, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_42, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_52, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_62, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_72, false);

            manager.Loop((int)reference, false);
            boolean1 = true;
            level8Finished = false;
        }

        if (!manager.IsPlaying((int)reference) && boolean1)
        {
            manager.Play((int)reference);
            manager.Play((int)newSound1);
            boolean1 = false;
            boolean2 = true;
        }

        if (!manager.IsPlaying((int)reference) && boolean2)
        {
            manager.Loop((int)reference, true);
            manager.Play((int)reference);
            manager.Play((int)newSound2);
            boolean2 = false;
        }
    }

    void PlayEnding(AudioManager.SoundLevel1 reference, AudioManager.SoundLevel1 newSound, ref bool boolean)
    {
        if (level9Finished)
        {
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_COMPLETE1, false);
            manager.Loop((int)AudioManager.SoundLevel1.SOUND_LEVEL1_COMPLETE2, false);

            manager.Loop((int)reference, false);
            boolean = true;
            level9Finished = false;
        }

        if (!manager.IsPlaying((int)reference) && boolean)
        {
            //manager.Loop(reference, true);
            //manager.Play(reference);
            manager.Play((int)newSound);
            boolean = false;
        }
    }
}
