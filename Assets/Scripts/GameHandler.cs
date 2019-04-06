using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    private static float startEnemyBlue = 2f;
    private static float startEnemyGreen = 4f;
    private static float startEnemyRed = 6f;

    private static float timerEnemyBlue = 5f;
    private static float timerEnemyGreen = 8f;
    private static float timerEnemyRed = 10f;

    private static float radius = 3f;

    private void Start()
    {
        this.SpawnEnemies();
    }

    private void SetEnemyTarget(Enemy enemy)
    {
        enemy.Setup(() => GameAssets.mInstance.GetPlayer().GetCurrentPosition());
    }

    private void SpawnEnemies()
    {
        InvokeRepeating("SpawnEnemyBlue", startEnemyBlue, timerEnemyBlue);
        InvokeRepeating("SpawnEnemyGreen", startEnemyGreen, timerEnemyGreen);
        InvokeRepeating("SpawnEnemyRed", startEnemyRed, timerEnemyRed);
    }

    private void SpawnEnemyBlue()
    {
        if (GameAssets.mInstance.GetPlayer().IsAlive())
        {
            EnemyBlue eBlue = EnemyBlue.Create(GameAssets.mInstance.GetPlayer().GetCurrentPosition() + Utils.GetRandomDir() * radius);
            this.SetEnemyTarget(eBlue);
        }
    }

    private void SpawnEnemyGreen()
    {
        if (GameAssets.mInstance.GetPlayer().IsAlive())
        {
            EnemyGreen eGreen = EnemyGreen.Create(GameAssets.mInstance.GetPlayer().GetCurrentPosition() + Utils.GetRandomDir() * radius);
            this.SetEnemyTarget(eGreen);
        }
    }

    private void SpawnEnemyRed()
    {
        if (GameAssets.mInstance.GetPlayer().IsAlive())
        {
            EnemyRed eRed = EnemyRed.Create(GameAssets.mInstance.GetPlayer().GetCurrentPosition() + Utils.GetRandomDir() * radius);
            this.SetEnemyTarget(eRed);
        }
    }

    private void Update()
    {
        this.HandleInputs();
    }

    private void HandleInputs()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
