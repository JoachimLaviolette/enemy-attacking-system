using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreen : Enemy
{
    private void Start()
    {
        this.mHealth = 20f;
        this.mSpeed = 1.5f;
        this.mDamages = 7.5f;
        SetupPrefab();
    }

    private static void SetupPrefab()
    {
        mPrefab = mPrefab = GameAssets.mInstance.GetEnemyGreen();
    }

    public static EnemyGreen Create(Vector3 spawnPosition)
    {
        SetupPrefab();
        Transform enemyTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        return enemyTransform.GetComponent<EnemyGreen>();
    }
}
