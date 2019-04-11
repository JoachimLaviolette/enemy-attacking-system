﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] protected Sprite[] frames;
    protected int currentFrame;
    protected float timer;

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer -= .1f;
            this.currentFrame++;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = frames[this.currentFrame];
            
            if (this.currentFrame == this.frames.Length - 1)
            {
                Array.Reverse(this.frames);
                this.currentFrame = 0;
            }
        }
    }
}
