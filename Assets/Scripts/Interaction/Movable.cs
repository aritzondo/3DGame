using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {

    public DropTrigger hook;

    private Light mLight;
    private AudioManager audioManager;

    // Use this for initialization
    void Start () {
        mLight = gameObject.GetComponentInChildren<Light>();
        audioManager = AudioManager.Instance;
    }
	
	public void Carry(Transform parent, Vector3 offset)
    {

        hook.empty = true;

        transform.SetParent(parent);
        transform.localPosition = offset;
        transform.rotation = parent.transform.rotation;
        if (mLight == null) return;
        
        CheckLightActive checkScript = GetComponentInChildren<CheckLightActive>();
        if (checkScript != null)
        {
            audioManager.Play((int)AudioManager.SoundGeneral.LIGHT_CLICK);
            checkScript.Deactivate();
        }
        else
        {
            mLight.enabled = false;
        }
    }

    public void Release()
    {
        hook.showDropSite(false);

        Transform dropPoint = hook.dropPoint;
        hook.empty = false;
        transform.parent = null;
        transform.position = dropPoint.position;
        CheckLightActive checkScript = GetComponentInChildren<CheckLightActive>();
        if (checkScript != null)
        {
            audioManager.Play((int)AudioManager.SoundGeneral.LIGHT_CLICK);
            checkScript.Activate();
        }
        else
        {
            mLight.enabled = true;
        }
        Rotable rot = gameObject.GetComponent<Rotable>();
        if(rot != null)
        {
            rot.released();
        }
    }
}
