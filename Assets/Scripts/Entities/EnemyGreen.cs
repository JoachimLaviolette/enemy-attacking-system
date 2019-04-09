using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : Enemy
{
    // Set up enemy green properties
    protected void Start()
    {
        this.mHealth = 20f;
        this.mSpeed = 1.5f;
        this.mDamages = 7.5f;
        this.mReward = 10;
        SetupPrefab();
    }

    // Set up enemy grene prefab
    private static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetEnemyGreen();
    }

    // Create a new instance of enemy green
    public static EnemyGreen Create(Vector3 spawnPosition)
    {
        SetupPrefab();
        Transform enemyTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        EnemyGreen enemy = enemyTransform.GetComponent<EnemyGreen>();
        Enemy.RecordEnemy(enemy);

        return enemy;
    }
}
