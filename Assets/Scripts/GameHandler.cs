using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    private static float startEnemyBlue = 2f;
    private static float startEnemyGreen = 3.5f;
    private static float startEnemyRed = 5f;

    private static float timerEnemyBlue = 3f;
    private static float timerEnemyGreen = 8f;
    private static float timerEnemyRed = 10f;

    private static float radius = 3f;

    // Set up game state
    private void Start()
    {
        this.SpawnEnemies();
    }

    // Set the new target the provided enemy has to follow
    private void SetEnemyTarget(Enemy enemy)
    {
        enemy.Setup(() => GameAssets.mInstance.GetPlayer().GetCurrentPosition());
    }

    // Call periodic functions to spwan randomly new enemy instances
    // At specific interval times
    private void SpawnEnemies()
    {
        InvokeRepeating("SpawnEnemyBlue", startEnemyBlue, timerEnemyBlue);
        InvokeRepeating("SpawnEnemyGreen", startEnemyGreen, timerEnemyGreen);
        InvokeRepeating("SpawnEnemyRed", startEnemyRed, timerEnemyRed);
    }

    // Instantiate an enemy blue randomly
    private void SpawnEnemyBlue()
    {
        if (GameAssets.mInstance.GetPlayer().IsAlive())
        {
            EnemyBlue eBlue = EnemyBlue.Create(GameAssets.mInstance.GetPlayer().GetCurrentPosition() + Utils.GetRandomDir() * radius);
            this.SetEnemyTarget(eBlue);
        }
    }

    // Instantiate an enemy green randomly
    private void SpawnEnemyGreen()
    {
        if (GameAssets.mInstance.GetPlayer().IsAlive())
        {
            EnemyGreen eGreen = EnemyGreen.Create(GameAssets.mInstance.GetPlayer().GetCurrentPosition() + Utils.GetRandomDir() * radius);
            this.SetEnemyTarget(eGreen);
        }
    }

    // Instantiate an enemy red randomly
    private void SpawnEnemyRed()
    {
        if (GameAssets.mInstance.GetPlayer().IsAlive())
        {
            EnemyRed eRed = EnemyRed.Create(GameAssets.mInstance.GetPlayer().GetCurrentPosition() + Utils.GetRandomDir() * radius);
            this.SetEnemyTarget(eRed);
        }
    }

    // Called every frame to update game state
    private void Update()
    {
        this.HandleInputs();
    }

    // Handle user inputs
    private void HandleInputs()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
