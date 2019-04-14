using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamagable, IDestroyable
{
    protected Vector3 lastMoveDir;
    protected float mHealth = 0f;
    protected float mSpeed = 0f;
    
    // Return the current position of the entity
    public Vector3 GetCurrentPosition()
    {
        return this.transform.position;
    }

    // Return the speed of the entity
    public float GetSpeed()
    {
        return this.mSpeed;
    }

    // Return the health of the entity
    public float GetHealth()
    {
        return this.mHealth;
    }

    // Damage the entity
    public virtual void Damage(float damages)
    {
        this.mHealth -= damages;

        // If the entity has no more health, kill it
        if (this.mHealth <= 0f)
        {
            this.Destroy();
        }
    }

    // Kill the entity
    public virtual void Destroy()
    {
        // Destroy the game object
        Destroy(gameObject);
    }

    // Tell if the entity is alive
    public bool IsAlive()
    {
        return this.mHealth > 0f;
    }
}
