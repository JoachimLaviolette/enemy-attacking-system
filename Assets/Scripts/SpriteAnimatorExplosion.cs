using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatorExplosion : SpriteAnimator
{
    private int counter;

    protected void Start()
    {
        this.counter = 0;
    }

    // Called every frame to update the current explosion's sprite
    protected override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= .01f)
        {
            timer -= .1f;
            this.currentFrame++;
            this.counter++;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = frames[this.currentFrame];

            if (this.currentFrame == this.frames.Length - 1)
            {
                Array.Reverse(this.frames);
                this.currentFrame = 0;
            }
        }
    }

    // Return if the associated particle explosion has to be destroyed or not
    public bool ToDestroy()
    {
        return this.counter >= (this.frames.Length * 4);
    }
}
