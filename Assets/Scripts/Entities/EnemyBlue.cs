using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlue : Enemy
{
    private void Start()
    {
        this.mHealth = 10f;
        this.mSpeed = 1f;
        this.mDamages = 5f;
        SetupPrefab();
    }

    private static void SetupPrefab()
    {
        mPrefab = mPrefab = GameAssets.mInstance.GetEnemyBlue();
    }

    public static EnemyBlue Create(Vector3 spawnPosition)
    {
        SetupPrefab();
        Transform enemyTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        return enemyTransform.GetComponent<EnemyBlue>();
    }
}
