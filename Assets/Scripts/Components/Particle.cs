using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour, IMovable, IDestroyable, ICollapsable
{
    protected static Particle mPrefab;
    protected Vector3 mTargetPosition;
    protected int mDamages;
    protected float mSpeed;
    protected Vector3 particleMoveDir;
    protected static List<Particle> particleList = new List<Particle>();

    // Const
    public const int PARTICLE_BASIC = 0;
    public const int PARTICLE_CANON = 1;
    public const int PARTICLE_EXPLOSION = 2;

    // Set up particle's properties
    public void Setup(Vector3 targetPosition)
    {
        this.mDamages = Random.Range(3, 10);
        this.mSpeed = 12f;
        this.SetTargetPosition(targetPosition);
        this.SetMoveDir();
    }

    // Called every frame to update the particle model
    protected virtual void Update()
    {
        this.HandleMovements();
        this.HandleCollisions();
    }

    // Automatically destroy the particle when off the screen
    private void OnBecameInvisible()
    {
        this.Destroy();
    }

    // Set the target position
    protected void SetTargetPosition(Vector3 targetPosition)
    {
        this.mTargetPosition = targetPosition;
    }

    // Set the moving direction of the particle
    protected void SetMoveDir()
    {
        this.particleMoveDir = (this.mTargetPosition - transform.position).normalized;
    }

    // Handle particle movement since it has been instantiated
    public void HandleMovements()
    {
        float distance = Vector3.Distance(this.mTargetPosition, transform.position);

        if (distance > 0f)
        {
            Vector3 newParticlePosition = transform.position + this.particleMoveDir * this.mSpeed * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newParticlePosition, this.mTargetPosition);

            if (distanceAfterMoving > distance)
            {
                // Overshot the target
                newParticlePosition = this.mTargetPosition;
            }

            newParticlePosition.z = 0f;
            transform.position = newParticlePosition;

            return;
        }
    }

    // Handle when the particle enters in collision with the enemy
    public virtual void HandleCollisions()
    {
        // Check if the particle position points an enemy
        Enemy enemy = Enemy.IsEnemyAt(transform.position);

        if (enemy)
        {
            bool isCritical = false;
            Utils.ApplyCritical(ref this.mDamages, ref isCritical);
            DamageNotification.Create(enemy.GetCurrentPosition(), this.mDamages, isCritical);
            enemy.Damage(this.mDamages);
            this.Destroy();
        }
    }

    // Destroy the particle
    public void Destroy()
    {
        // Remove the current particle instance from the particles list
        particleList.Remove(this);

        // Destroy the game object
        Destroy(gameObject);
    }

    // Return the current position of the particle
    public Vector3 GetCurrentPosition()
    {
        return this.transform.position;
    }

    // Set up particle prefab
    private static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetParticle(PARTICLE_BASIC);
    }

    // Create a new instance of particle
    public static Particle Create(Vector3 targetPosition)
    {
        SetupPrefab();

        Vector3 spawnPosition = Utils.GetShootPosition();
        Transform particleTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);
        Particle particle = particleTransform.GetComponent<Particle>();
        particle.Setup(targetPosition);
        Particle.RecordParticle(particle);

        return particle;
    }

    // Record a new particle instance
    public static void RecordParticle(Particle particle)
    {
        // When an enemy is instantiated add it to the list of enemies
        particleList.Add(particle);
    }

    // Remove the particle from the particle list
    public static void RemoveParticle(Particle particle)
    {
        particleList.Remove(particle);
    }

    // Check if there is a particle at the target position
    // Return the targeted particle instance if found
    public static Particle IsParticleAt(Vector3 targetPosition)
    {
        Vector3 newTargetPosition = targetPosition;
        newTargetPosition.z = 0f;

        float maxRange;

        foreach (Particle particle in particleList)
        {
            maxRange = Utils.GetSpriteSize(particle.gameObject).x / 2f;
            Vector3 particlePosition = particle.GetCurrentPosition();
            particlePosition.z = newTargetPosition.z;
            float distance = Vector3.Distance(newTargetPosition, particlePosition);

            if (distance <= maxRange)
            {
                return particle;
            }
        }

        return null;
    }

    // Check if there is a particle in the provided range
    public static Particle IsParticleAt(Vector3 targetPosition, float maxRange)
    {
        Vector3 newTargetPosition = targetPosition;

        foreach (Particle particle in particleList)
        {
            Vector3 particlePosition = particle.GetCurrentPosition();
            particlePosition.z = newTargetPosition.z = 0f;
            float distance = Vector3.Distance(newTargetPosition, particlePosition);

            if (distance <= maxRange)
            {
                return particle;
            }
        }

        return null;
    }
}
