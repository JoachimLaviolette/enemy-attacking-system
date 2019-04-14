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
    private static float startItemHealth = 4f;
    private static float startItemArmor = 7f;

    private static float timerEnemyBlue = 3f;
    private static float timerEnemyGreen = 8f;
    private static float timerEnemyRed = 10f;
    private static float timerItemHealth = 15f;
    private static float timerItemArmor = 20f;

    private static float radius = 3f;

    private Player mPlayer;

    // Set up game state
    private void Start()
    {
        this.mPlayer = GameAssets.mInstance.GetPlayer();
        this.SpawnEnemies();
        this.SpawnItems();
    }

    // Call periodic functions to spwan randomly new enemy instances
    // At specific interval times
    private void SpawnEnemies()
    {
        FunctionPeriodic.Create(() => EnemyBlue.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDir() * radius), startEnemyBlue, timerEnemyBlue);
        FunctionPeriodic.Create(() => EnemyGreen.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDir() * radius), startEnemyGreen, timerEnemyGreen);
        FunctionPeriodic.Create(() => EnemyRed.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDir() * radius), startEnemyRed, timerEnemyRed);
    }

    // Call periodic functions to spwan randomly new bonus items
    // At random interval times
    private void SpawnItems()
    {
        FunctionPeriodic.Create(() => ItemHealth.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDirItemBonus() * radius), startItemHealth, timerItemHealth);
        FunctionPeriodic.Create(() => ItemArmor.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDirItemBonus() * radius), startItemArmor, timerItemArmor);
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
