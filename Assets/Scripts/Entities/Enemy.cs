using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : Entity
{
    protected static Enemy mPrefab = null;
    protected float mDamages;
    protected Vector3 playerPosition;
    protected Vector3 enemyPosition;
    protected Vector3 enemyMoveDir;

    private Func<Vector3> GetPlayerPositionFunc;
    
    private void Update()
    {
        UpdatePositionsData();
        HandleMovements();
        HandleCollision();
    }

    public void Setup(Func<Vector3> GetPlayerPositionFunc)
    {
        this.GetPlayerPositionFunc = GetPlayerPositionFunc;
    }

    public void SetGetPlayerPositionFunc(Func<Vector3> GetPlayerPositionFunc)
    {
        this.GetPlayerPositionFunc = GetPlayerPositionFunc;
    }

    public void UpdatePositionsData()
    {
        this.playerPosition = GetPlayerPositionFunc();
        this.playerPosition.z = transform.position.z;

        this.enemyPosition = playerPosition;
        this.enemyPosition.z = transform.position.z;
    }

    // Basically all enemy entities seek to enter in collision with the player
    protected override void HandleMovements()
    {
        this.enemyMoveDir = (this.enemyPosition - transform.position).normalized;
        float distance = Vector3.Distance(this.enemyPosition, transform.position);

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

    protected void HandleCollision()
    {
        float distance = Vector3.Distance(this.enemyPosition, transform.position);

        if (distance <= 0f)
        {
            // Damage the player
            GameAssets.mInstance.GetPlayer().Damage(this.mDamages);
            // Destruct the enemy
            Destroy(gameObject);
        }
    }
}
