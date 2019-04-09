using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : ItemBonus
{
    // Set up item health's properties
    protected override void Start()
    {
        base.Start();
        this.mReward = 8f;
    }

    // Apply the item health's effect to the player
    public override void ApplyEffect(Player player)
    {
        player.AddHealth(this.mReward);
    }

    // Set up health item's prefab
    protected new static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetItemHealth();
    }

    // Instantiate a new item health at the provided spawn position
    public static ItemHealth Create(Vector3 spawnPosition)
    {
        SetupPrefab();
        Transform itemTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        ItemHealth item = itemTransform.GetComponent<ItemHealth>();
        Item.RecordEnemy(item);

        return item;
    }
}
