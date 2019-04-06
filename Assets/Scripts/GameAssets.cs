using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets mInstance;
    public Player mPlayer;
    public EnemyBlue pf_EnemyBlue;
    public EnemyGreen pf_EnemyGreen;
    public EnemyRed pf_EnemyRed;

    public Sprite[] sprite_Health;

    private void Awake()
    {
        mInstance = this;
    }

    public Player GetPlayer()
    {
        return this.mPlayer;
    }

    public Enemy[] GetEnemies()
    {
        Enemy[] enemies =
        {
            this.pf_EnemyBlue, this.pf_EnemyGreen, this.pf_EnemyRed
        };

        return enemies;
    }

    public EnemyBlue GetEnemyBlue()
    {
        return this.pf_EnemyBlue;
    }

    public EnemyGreen GetEnemyGreen()
    {
        return this.pf_EnemyGreen;
    }

    public EnemyRed GetEnemyRed()
    {
        return this.pf_EnemyRed;
    }

    public Sprite[] GetHealthSprites()
    {
        return this.sprite_Health;
    }

    public Sprite GetHealthSprite(int healthLevel)
    {
        return this.sprite_Health[healthLevel];
    }
}
