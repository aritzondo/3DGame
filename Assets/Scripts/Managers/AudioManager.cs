using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour {

    #region Public Attributes
    
    public Sound[] sounds;
    public enum SoundLevel1
    {
        SOUND_LEVEL1_11 = 0,
        SOUND_LEVEL1_12 = 0,
        SOUND_LEVEL1_21 = 0,
        SOUND_LEVEL1_22 = 0,
        SOUND_LEVEL1_31 = 0,
        SOUND_LEVEL1_32 = 0,
        SOUND_LEVEL1_41 = 0,
        SOUND_LEVEL1_42 = 0,
        SOUND_LEVEL1_51 = 0,
        SOUND_LEVEL1_52 = 0,
        SOUND_LEVEL1_61 = 0,
        SOUND_LEVEL1_62 = 0,
        SOUND_LEVEL1_71 = 0,
        SOUND_LEVEL1_72 = 0,
        SOUND_LEVEL1_COMPLETE1 = 0,
        SOUND_LEVEL1_COMPLETE2 = 0,
        SOUND_LEVEL1_ENDING = 0,
        SOUND_LEVEL1_REFERENCE = 0
    }

    public static AudioManager instance;

    #endregion

    #region Private Attributes

    #endregion

    #region Properties

    #endregion

    #region Monobehaviour Methods

    // Use this for initialization
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}

    void Start()
    {
        
    }

    #endregion

    #region Methods

    public void Play(SoundLevel1 selected)
    {
        sounds[(int)selected].source.Play();
        /*for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name.Equals(name))
            {
                sounds[i].source.Play();
            }
        }*/
    }

    public void Volume(SoundLevel1 selected, float newVolume)//(string name, float newVolume)
    {
        sounds[(int)selected].source.volume = newVolume;
        /*for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name.Equals(name))
            {
                sounds[i].source.volume = newVolume;
            }
        }*/
    }

    public void Pitch(SoundLevel1 selected, float newPitch)
    {
        sounds[(int)selected].source.pitch = newPitch;
    }

    public void Loop(SoundLevel1 selected, bool newLoop)
    {
        sounds[(int)selected].source.loop = newLoop;
    }

    public bool IsPlaying(SoundLevel1 selected)
    {
        return sounds[(int)selected].source.isPlaying;
    }

    #endregion
}
