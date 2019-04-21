using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimatorEnemyShield : SpriteAnimator
{
    private Enemy mEnemy;
    [SerializeField] private Sprite mBasicSprite;
    private bool playAnimation;
    private int counter;

    protected void Start()
    {
        this.counter = 0;
        this.playAnimation = false;
    }

    protected override void Update()
    {
        if (!this.playAnimation)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = mBasicSprite;

            return;
        }

        timer += Time.deltaTime;

        if (timer >= .1f)
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

        if (this.counter >= (this.frames.Length * 4))
        {
            this.StopAnimation();
        }
    }
    
    public void PlayAnimation()
    {
        this.playAnimation = true;
    }

    private void StopAnimation()
    {
        this.playAnimation = false;

        if (this.mEnemy)
        {
            ((EnemyRed)this.mEnemy).TurnOffShield();
        }
    }

    public void SetEnemy(Enemy enemy)
    {
        this.mEnemy = enemy;
    }
}
