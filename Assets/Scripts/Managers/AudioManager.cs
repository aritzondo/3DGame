using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    #region Public Attributes

    public Sound[] sounds;

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
        Play("Theme");
    }

    #endregion

    #region Methods

    public void Play (string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name.Equals(name))
            {
                sounds[i].source.Play();
            }
        }
    }

    #endregion
}
