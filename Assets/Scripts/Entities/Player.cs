using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{ 
    // Player score
    private int mScore;

    // Const
    public static float TOTAL_HEALTH = 30f;

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveY += 1f;
        }

        // down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveY -= 1f;
        }

        // left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX -= 1f;
        }

        // right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX += 1f;
        }

        bool isIdle = moveX == 0 && moveY == 0;

        Vector3 moveDir = new Vector3(moveX, moveY).normalized;
        this.lastMoveDir = moveDir;
        transform.position += moveDir * this.mSpeed * Time.deltaTime;
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

    // Damage the player of a certain amount
    public void Damage(float damages)
    {
        this.mHealth -= damages;
        Debug.Log("Player lost: " + damages + " HP");
        Debug.Log("Current health: " + this.mHealth + " HP");

        // If the player has no more health, kill him
        if (this.mHealth <= 0f)
        {
            this.Kill();
        }
    }

    // Kill the player
    private void Kill()
    {

    }

    public bool IsAlive()
    {
        return this.mHealth >= 0f;
    }
}
