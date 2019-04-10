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

    // Set the new target the provided enemy has to follow
    private void SetEnemyTarget(Enemy enemy)
    {
        enemy.Setup(() => this.mPlayer.GetCurrentPosition());
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
        if (this.mPlayer.IsAlive())
        {
            EnemyBlue eBlue = EnemyBlue.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDir() * radius);
            this.SetEnemyTarget(eBlue);
        }
    }

    // Instantiate an enemy green randomly
    private void SpawnEnemyGreen()
    {
        if (this.mPlayer.IsAlive())
        {
            EnemyGreen eGreen = EnemyGreen.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDir() * radius);
            this.SetEnemyTarget(eGreen);
        }
    }

    // Instantiate an enemy red randomly
    private void SpawnEnemyRed()
    {
        if (this.mPlayer.IsAlive())
        {
            EnemyRed eRed = EnemyRed.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDir() * radius);
            this.SetEnemyTarget(eRed);
        }
    }

    // Call periodic functions to spwan randomly new bonus items
    // At random interval times
    private void SpawnItems()
    {
        InvokeRepeating("SpawnItemHealth", startItemHealth, timerItemHealth);
        InvokeRepeating("SpawnItemArmor", startItemArmor, timerItemArmor);
    }

    // Instantiate a health item randomly
    private void SpawnItemHealth()
    {
        if (this.mPlayer.IsAlive())
        {
            ItemHealth.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDirItemBonus() * radius);
        }
    }

    // Instantiate a armor item randomly
    private void SpawnItemArmor()
    {
        if (this.mPlayer.IsAlive())
        {
            ItemArmor.Create(this.mPlayer.GetCurrentPosition() + Utils.GetRandomDirItemBonus() * radius);
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
