using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound{

    #region Public Attributes

    public string name;

    public AudioClip clip;

    [Range(0.0f, 1.0f)]
    public float volume = 1.0f;
    [Range(0.1f, 3.0f)]
    public float pitch = 1.0f;

    public bool loop = true;

    [HideInInspector]
    public AudioSource source;

	#endregion
	
	#region Private Attributes
	
	#endregion
	
	#region Properties
	
	#endregion
	
	#region Methods
	
	#endregion
}
