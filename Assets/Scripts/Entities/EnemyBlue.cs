using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : Enemy
{
    // Set up enemy blue properties
    protected void Start()
    {
        this.mHealth = 10f;
        this.mSpeed = 1f;
        this.mDamages = 5;
        this.mReward = 5;
        this.mAI = new AIBlue(this);
    }

    // Set up enemy blue prefab
    protected new static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetEnemyBlue();
    }

    // Create a new instance of enemy blue
    public static EnemyBlue Create(Vector3 spawnPosition)
    {
        Player player = GameAssets.mInstance.GetPlayer();

        if (!player.IsAlive())
        {
            return null;
        }

        SetupPrefab();

        Transform enemyTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        EnemyBlue enemy = enemyTransform.GetComponent<EnemyBlue>();
        enemy.Setup(() => player.GetCurrentPosition());

        Enemy.RecordEnemy(enemy);

        return enemy;
    }
}
