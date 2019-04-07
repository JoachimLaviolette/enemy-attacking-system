using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{ 
    // Player score
    private int mScore;

    // Const
    public static float TOTAL_HEALTH = 30f;

    // Called every frame to update player model
    private void Update()
    {
        this.HandleMovements();
        this.HandleActions();
    }

    // Set up player properties
    private void Start()
    {
        this.mHealth = TOTAL_HEALTH;
        this.mSpeed = 4.5f;
    }

    // Handle input movements
    protected override void HandleMovements()
    { 
        float moveX = 0f;
        float moveY = 0f;

        // up
        if (Input.GetKey(KeyCode.Z))
        {
            moveY += 1f;
        }

        // down
        if (Input.GetKey(KeyCode.S))
        {
            moveY -= 1f;
        }

        // left
        if (Input.GetKey(KeyCode.Q))
        {
            moveX -= 1f;
        }

        // right
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }

        bool isIdle = moveX == 0 && moveY == 0;

        Vector3 moveDir = new Vector3(moveX, moveY).normalized;
        this.lastMoveDir = moveDir;
        transform.position += moveDir * this.mSpeed * Time.deltaTime;
    }

    // Handle player's actions
    private void HandleActions()
    {
        // If mouse's left button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse position points an enemy
            Enemy enemy = Enemy.IsEnemyAt(Utils.GetWorldPosition((Input.mousePosition)));

            if (enemy)
            {
                Debug.Log("Enemy found! About to damage it!");
                enemy.Damage(5f);
            }
        }
    }

    // Get the score of the player
    public int GetScore()
    {
        return this.mScore;
    }

    // Get the health of the player
    public float GetHealth()
    {
        return this.mHealth;
    }

    // Set the score of the player
    public void SetScore(int score)
    {
        this.mScore = score;
    }
}
