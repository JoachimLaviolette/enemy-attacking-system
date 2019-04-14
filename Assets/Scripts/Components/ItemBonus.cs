using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ItemBonus : Item
{
    // Reward the item provides
    protected float mReward;

    // Set up the bonus item properties
    protected override void Start()
    {
        base.Start();
        this.mSpeed = 1f;
        this.mReward = 0f;
        this.moveDir = Vector3.down;
    }

    // Handle the bonus item movements
    public override void HandleMovements()
    {
        Vector3 newItemPosition = transform.position + this.moveDir * this.mSpeed * Time.deltaTime;
        newItemPosition.z = 0f;
        transform.position = newItemPosition;
    }

    // Handle the bonus item collisions
    public override void HandleCollisions()
    {
        Player player = Player.IsPlayerAt(transform.position);

        // Check if item position points the player
        if (player)
        {
            this.ApplyEffect(player);
            this.Destroy();
        }
    }

    // Set up the bonus item's prefab
    protected static void SetupPrefab()
    {
        mPrefab = null;
    }
}
