using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : Enemy
{
    // Set up enemy red properties
    protected void Start()
    {
        this.mHealth = 30f;
        this.mSpeed = 2f;
        this.mDamages = 10f;
        this.mReward = 20;
    }

    // Set up enemy red prefab
    private static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetEnemyRed();
    }

    // Create a new instance of enemy red
    public static EnemyRed Create(Vector3 spawnPosition)
    {
        SetupPrefab();
        Transform enemyTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        EnemyRed enemy = enemyTransform.GetComponent<EnemyRed>();
        Enemy.RecordEnemy(enemy);

        return enemy;
    }
}
