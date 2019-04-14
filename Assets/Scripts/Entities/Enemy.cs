using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : Entity, ICollapsable
{
    protected static Enemy mPrefab = null;
    protected float mDamages;
    protected int mReward;
    protected Vector3 playerPosition;
    protected Vector3 enemyPosition;
    protected Vector3 enemyMoveDir;
    protected Func<Vector3> GetPlayerPositionFunc;
    protected static List<Enemy> enemyList = new List<Enemy>();
    protected AI mAI;
    
    // Called every frame to update the enemy model
    protected void Update()
    {
        this.UpdatePositions();
        this.HandleCollisions();
        this.mAI.Update();        
    }

    // Set up the focus target function
    protected void Setup(Func<Vector3> GetPlayerPositionFunc)
    {
        this.GetPlayerPositionFunc = GetPlayerPositionFunc;
    }

    // Set the focus target function
    public void SetGetPlayerPositionFunc(Func<Vector3> GetPlayerPositionFunc)
    {
        this.GetPlayerPositionFunc = GetPlayerPositionFunc;
    }

    // Update position data attributes
    // Of the enemy and the player
    protected void UpdatePositions()
    {
        this.playerPosition = GetPlayerPositionFunc();
        this.playerPosition.z = transform.position.z;

        this.enemyPosition = playerPosition;
        this.enemyPosition.z = transform.position.z;
    }

    // Handle when the enemy enters in collision with the player
    public void HandleCollisions()
    {
        // Computes the distance between the player and the enemy
        float distance = Vector3.Distance(this.playerPosition, transform.position);

        // If the enemy enters in collision with the player
        if (distance <= 0f)
        {
            // Damage the player
            GameAssets.mInstance.GetPlayer().Damage(this.mDamages);

            // Then automatically destroy the enemy after the collision
            this.Destroy();
        }
    }

    // Damage the player
    public override void Damage(float damages)
    {
        base.Damage(damages);

        if (!this.IsAlive())
        {
            Player player = GameAssets.mInstance.GetPlayer();
            player.SetScore(player.GetScore() + this.mReward);
        }
    }

    // Kill the enemy
    public override void Destroy()
    {        
        // Remove the current enemy instance from the enemy list
        enemyList.Remove(this);

        Vector3 spawnPosition = this.transform.position;

        base.Destroy();
                     
        // Create an explosion
        ParticleExplosion.Create(spawnPosition);
    }

    // Check if there is an enemy at the target position
    // Return the targeted enemy instance if found
    public static Enemy IsEnemyAt(Vector3 targetPosition)
    {
        Vector3 newTargetPosition = targetPosition;

        float maxRange;

        foreach (Enemy enemy in enemyList)
        {
            maxRange = Utils.GetSpriteSize(enemy.gameObject).x / 2f;
            Vector3 enemyPosition = enemy.GetCurrentPosition();
            enemyPosition.z = newTargetPosition.z = 0f;
            float distance = Vector3.Distance(newTargetPosition, enemyPosition);

            if (distance <= maxRange)
            {
                return enemy;
            }
        }

        return null;
    }

    // Record a new enemy instance
    public static void RecordEnemy(Enemy enemy)
    {
        // When an enemy is instantiated add it to the list of enemies
        enemyList.Add(enemy);
    }

    // Remove the enemy from the enemy list
    public static void RemoveEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    // Set up the enemy prefab
    protected static void SetupPrefab()
    {
        mPrefab = null;
    }
}
