using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    #region Public Attributes

    public GameObject player;
    public LevelsMusicManager musicManager;
    public Sound[] sounds = new Sound[(int) SoundLevel1.COUNT];

    public enum SoundLevel1
    {
        SOUND_LEVEL1_11 = 0,
        SOUND_LEVEL1_12,
        SOUND_LEVEL1_21,
        SOUND_LEVEL1_22,
        SOUND_LEVEL1_31,
        SOUND_LEVEL1_32,
        SOUND_LEVEL1_41,
        SOUND_LEVEL1_42,
        SOUND_LEVEL1_51,
        SOUND_LEVEL1_52,
        SOUND_LEVEL1_61,
        SOUND_LEVEL1_62,
        SOUND_LEVEL1_71,
        SOUND_LEVEL1_72,
        SOUND_LEVEL1_COMPLETE1,
        SOUND_LEVEL1_COMPLETE2,
        SOUND_LEVEL1_ENDING,
        SOUND_LEVEL1_REFERENCE,

        COUNT
    }

    public enum SoundGeneral
    {
        PASO1 = SoundLevel1.COUNT,
        PASO2,
        DOOR_VERTICAL,
        DOOR_HORIZONTAL,
        LIGHT_CLICK,

        COUNT
    }

    #endregion

    

    #region Private Attributes
    private static AudioManager instance;
    private GameObject soundList;
    #endregion

    #region Properties

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<AudioManager>();
            }

            return instance;
        }
    }

    #endregion

    #region Monobehaviour Methods

    // Use this for initialization
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            Debug.Log("2 instances of AudioManager detected. Destroying script at " + gameObject.name);
            return;
            
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        soundList = transform.GetChild(0).gameObject;

        foreach (Sound s in sounds)
        {
            s.source = soundList.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    #endregion

    #region Methods

    public void Play(int selected)
    {
        sounds[(int) selected].source.Play();
    }

    public void Stop(int selected)
    {
        sounds[(int) selected].source.Stop();
    }

    public void Volume(int selected, float newVolume)
    {
        sounds[(int) selected].volume = newVolume;
        sounds[(int) selected].source.volume = sounds[(int) selected].volume;
    }

    public void Pitch(int selected, float newPitch)
    {
        sounds[(int) selected].pitch = newPitch;
        sounds[(int) selected].source.pitch = sounds[(int) selected].pitch;
    }

    public void Loop(int selected, bool newLoop)
    {
        sounds[(int) selected].loop = newLoop;
        sounds[(int) selected].source.loop = sounds[(int) selected].loop;
    }

    public bool IsPlaying(int selected)
    {
        return sounds[(int) selected].source.isPlaying;
    }

    public void MasterVolume(float newVolume)
    {
        foreach (Sound s in sounds)
        {
            s.volume = newVolume;
            s.source.volume = s.volume;
        }
    }

    #endregion
}