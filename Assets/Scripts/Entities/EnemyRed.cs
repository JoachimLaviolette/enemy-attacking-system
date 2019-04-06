using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : Enemy
{
    private void Start()
    {
        this.mHealth = 30f;
        this.mSpeed = 2f;
        this.mDamages = 10f;
        SetupPrefab();
    }

    private static void SetupPrefab()
    {
        mPrefab = mPrefab = GameAssets.mInstance.GetEnemyRed();
    }

    public static EnemyRed Create(Vector3 spawnPosition)
    {
        SetupPrefab();
        Transform enemyTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        return enemyTransform.GetComponent<EnemyRed>();
    }
}
