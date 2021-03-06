﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : Enemy
{
    // Set up enemy blue properties
    private void Start()
    {
        this.mHealth = 10f;
        this.mSpeed = 1f;
        this.mDamages = 5f;
        this.mReward = 5;
        SetupPrefab();
    }

    // Set up enemy blue prefab
    private static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetEnemyBlue();
    }

    // Create a new instance of enemy blue
    public static EnemyBlue Create(Vector3 spawnPosition)
    {
        SetupPrefab();
        Transform enemyTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        EnemyBlue enemy = enemyTransform.GetComponent<EnemyBlue>();
        Enemy.RecordEnemy(enemy);

        return enemy;
    }
}
