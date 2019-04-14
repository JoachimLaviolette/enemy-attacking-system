using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class AI
{
    // Reference to the player
    protected Player mPlayer;
    protected Enemy mEnemy;

    // Constructor
    protected AI(Enemy enemy)
    {
        this.mPlayer = GameAssets.mInstance.GetPlayer();
        this.mEnemy = enemy;
    }

    // Update the AI
    abstract public void Update();
    // Adjust the position of the enemy if about to collapse a particle
    abstract protected void CheckNewPosition(ref Vector3 newPosition);
}
