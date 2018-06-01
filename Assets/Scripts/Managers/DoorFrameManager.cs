using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFrameManager : MonoBehaviour {

    
    public List<GameObject> doorFrames;

    private int levelsCompleted = 0;
    private static DoorFrameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            Debug.Log("2 instances of DoorFrameManager detected. Destroying script at " + this.gameObject.name);
            return;
        }
    }

    public static DoorFrameManager getInstance()
    {
        return instance;
    }

    public int addCompleteLevel(int newComplete)
    {
        doorFrames[newComplete].GetComponent<LevelCompletedCheck>().FinishedLevel();
        ++levelsCompleted;
        return levelsCompleted;
    }
}
