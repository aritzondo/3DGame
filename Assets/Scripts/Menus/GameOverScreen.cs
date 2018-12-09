﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour 
{
    public float FadeRate;
    private RawImage image;
    private float targetAlpha;
    // Use this for initialization
    private void Start()
    {
        this.image = this.GetComponent<RawImage>();
        if (this.image == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }
        this.targetAlpha = this.image.color.a;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            FadeIn();
        }
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (!(alphaDiff > 0.0001f)) return;
        
        curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
        this.image.color = curColor;
    }

    public void FadeOut()
    {
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        this.targetAlpha = 1.0f;
    }
}
