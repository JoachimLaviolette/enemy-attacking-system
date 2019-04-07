using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Vector3 lastMoveDir;
    protected float mHealth = 0f;
    protected float mSpeed = 0f;

    // Called every frame to update entity model
    private void Update()
    {
        this.HandleMovements();
    }

    // Return the current position of the entity
    public Vector3 GetCurrentPosition()
    {
        return this.transform.position;
    }

    // Damage the entity
    public virtual void Damage(float damages)
    {
        this.mHealth -= damages;

        // If the entity has no more health, kill it
        if (this.mHealth <= 0f)
        {
            this.Kill();
        }
    }

    // Kill the entity
    protected virtual void Kill()
    {
        // Destroy the game object
        Destroy(gameObject);
    }

    // Tell if the entity is alive
    public bool IsAlive()
    {
        return this.mHealth > 0f;
    }

    abstract protected void HandleMovements();
}
