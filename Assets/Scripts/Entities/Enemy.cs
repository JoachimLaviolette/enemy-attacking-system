using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : Entity
{
    protected static Enemy mPrefab = null;
    protected float mDamages;
    protected int mReward;
    protected Vector3 playerPosition;
    protected Vector3 enemyPosition;
    protected Vector3 enemyMoveDir;
    protected Func<Vector3> GetPlayerPositionFunc;
    protected static List<Enemy> enemyList = new List<Enemy>();
    
    // Called every frame to update the enemy model
    protected void Update()
    {
        UpdatePositions();
        HandleMovements();
        HandleCollision();
    }

    // Set up the focus target function
    public void Setup(Func<Vector3> GetPlayerPositionFunc)
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

    // Handle enemy movements to make it enter in collision with the player
    protected override void HandleMovements()
    {
        this.enemyMoveDir = (this.enemyPosition - transform.position).normalized;
        float distance = Vector3.Distance(this.playerPosition, transform.position);

        if (distance > 0f)
        {
            Vector3 newEnnemyPosition = transform.position + this.enemyMoveDir * this.mSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newEnnemyPosition, this.enemyPosition);

            if (distanceAfterMoving > distance)
            {
                // Overshot the target
                newEnnemyPosition = this.enemyPosition;
            }

            newEnnemyPosition.z = 0f;
            transform.position = newEnnemyPosition;

            return;
        }
    }

    // Handle when the enemy enters in collision with the player
    protected void HandleCollision()
    {
        // Computes the distance between the player and the enemy
        float distance = Vector3.Distance(this.playerPosition, transform.position);

        // If the enemy enters in collision with the player
        if (distance <= 0f)
        {
            // Damage the player
            GameAssets.mInstance.GetPlayer().Damage(this.mDamages);

            // Then automatically destroy the enemy after the collision
            this.Kill();
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
    protected override void Kill()
    {        
        // Remove the current enemy instance from the enemy list
        enemyList.Remove(this);

        base.Kill();
    }

    // Check if there is an enemy at the target position
    // Return the enemy targeted if one is found
    public static Enemy IsEnemyAt(Vector3 targetPosition)
    {
        Vector3 newTargetPosition = targetPosition;
        newTargetPosition.z = 0f;
        float maxRange;

        foreach (Enemy enemy in enemyList)
        {
            maxRange = Utils.GetSpriteSize(enemy.gameObject).x / 2f;
            float distance = Vector3.Distance(newTargetPosition, enemy.GetCurrentPosition());

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
