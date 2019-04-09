using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Item : MonoBehaviour
{
    // The speed the item moves at
    protected float mSpeed;
    // Moving dir
    protected Vector3 moveDir;
    // Player instance
    protected Player mPlayer;
    // Item's prefab
    protected static Item mPrefab = null;
    // List of the existing items
    protected static List<Item> itemList = new List<Item>();

    // Set up the item properties
    protected virtual void Start()
    {
        this.mPlayer = GameAssets.mInstance.GetPlayer();
    }

    // Called every frame to update the item model
    protected virtual void Update()
    {
        if (itemList.IndexOf(this) >= 0)
        {
            this.HandleMovements();
            this.HandleCollisions();
        }
    }

    // Automatically destroy the iyrm when off the screen
    private void OnBecameInvisible()
    {
        this.Destroy();
    }

    // Destroy the item
    protected void Destroy()
    {
        // Remove the current item instance from the items list
        itemList.Remove(this);

        // Destroy the game object
        Destroy(gameObject);
    }


    // Record a new item instance
    public static void RecordEnemy(Item item)
    {
        // When an item is instantiated add it to the list of items
        itemList.Add(item);
    }

    // Remove the item from the item list
    public static void RemoveEnemy(Item item)
    {
        itemList.Remove(item);
    }

    // Handle the item movements
    abstract protected void HandleMovements();

    // Handle the item collisions
    abstract protected void HandleCollisions();

    // Apply item effect
    abstract public void ApplyEffect(Player player);
}
