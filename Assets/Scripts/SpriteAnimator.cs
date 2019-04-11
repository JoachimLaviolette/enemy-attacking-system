using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    private int currentFrame;
    private float timer;

    private void Update()
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
