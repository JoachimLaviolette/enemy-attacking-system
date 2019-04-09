using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    protected static Particle mPrefab;
    protected Vector3 mTargetPosition;
    protected float mDamages = 5f;
    protected float mSpeed = 12f;
    protected Vector3 particleMoveDir;
    protected static List<Particle> particleList = new List<Particle>();

    // Const
    public const int PARTICLE_BASIC = 0;
    public const int PARTICLE_CANON = 1;

    // Called every frame to update the particle model
    protected void Update()
    {
        if (particleList.IndexOf(this) >= 0)
        {
            this.HandleMovements();
            this.HandleCollisions();
        }
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
    protected void HandleMovements()
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

    // Automatically destroy the particle when off the screen
    private void OnBecameInvisible()
    {
        this.Destroy();
    }

    // Handle when the particle enters in collision with the enemy
    protected void HandleCollisions()
    {
        // Check if the particle position points an enemy
        Enemy enemy = Enemy.IsEnemyAt(transform.position);

        if (enemy)
        {
            enemy.Damage(this.mDamages);
            this.Destroy();
        }
    }

    // Destroy the particle
    protected void Destroy()
    {
        // Remove the current particle instance from the particles list
        particleList.Remove(this);

        // Destroy the game object
        Destroy(gameObject);
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
        Transform particleTransform = Instantiate(mPrefab.transform, GameAssets.mInstance.GetPlayer().GetCurrentPosition(), Quaternion.identity);
        Particle particle = particleTransform.GetComponent<Particle>();
        particle.SetTargetPosition(targetPosition);
        particle.SetMoveDir();
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
}
