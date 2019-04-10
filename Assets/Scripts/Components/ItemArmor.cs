using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemArmor : ItemBonus
{
    // Set up item armor's properties
    protected override void Start()
    {
        base.Start();
        this.mReward = Player.TOTAL_ARMOR / 3f;
    }

    // Apply item armor's effect to the player
    public override void ApplyEffect(Player player)
    {
        player.AddArmor(this.mReward);
    }

    // Set up health armors's prefab
    protected new static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetItemArmor();
    }

    // Instantiate a new item armor at the provided spawn position
    public static ItemArmor Create(Vector3 spawnPosition)
    {
        SetupPrefab();
        Transform itemTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        ItemArmor item = itemTransform.GetComponent<ItemArmor>();
        Item.RecordEnemy(item);

        return item;
    }
}
