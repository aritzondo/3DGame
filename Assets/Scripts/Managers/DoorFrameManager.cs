﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFrameManager : MonoBehaviour {
    
    public List<GameObject> doorFrames;
    public PauseMenu pMenu;

    private int levelsCompleted;
    private static DoorFrameManager instance;
    private int newLevelStarted;

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
        levelsCompleted = 0;
    }

    public static DoorFrameManager GetInstance()
    {
        return instance;
    }

    public int AddCompleteLevel()
    {
        doorFrames[newLevelStarted].GetComponentInChildren<LevelCompletedCheck>().FinishedLevel();
        ++levelsCompleted;
        if (levelsCompleted >= 8)
        {
            pMenu.GameOver = true;
            gameObject.GetComponent<LevelsMusicManager>().levelFinished(9);
        }
        return levelsCompleted;
    }

    public void NewLevel(int levelNumber)
    {
        newLevelStarted = levelNumber;
    }
}
