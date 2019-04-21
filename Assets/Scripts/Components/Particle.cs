using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour, IMovable, IDestroyable, ICollapsable
{
    protected static Particle mPrefab;
    protected Vector3 mTargetPosition;
    protected int mDamages;
    protected float mSpeed;
    protected Entity mHostEntity;
    protected bool fromPlayer;
    protected Vector3 particleMoveDir;
    protected static List<Particle> particleList = new List<Particle>();

    // Const
    public const int PARTICLE_BASIC = 0;
    public const int PARTICLE_BASIC_ENEMY = 1;
    public const int PARTICLE_CANON = 2;
    public const int PARTICLE_EXPLOSION = 3;

    // Set up particle's properties
    public void Setup(Vector3 targetPosition, Entity hostEntity)
    {
        this.mHostEntity = hostEntity;
        this.fromPlayer = mHostEntity.GetType() == GameAssets.mInstance.mPlayer.GetType();
        this.SetDamages();
        this.SetSpeed();
        this.SetTargetPosition(targetPosition);
        this.SetMoveDir();
    }

    // Called every frame to update the particle model
    protected virtual void Update()
    {
        this.HandleMovements();
        this.HandleCollisions();
    }

    // Automatically destroy the particle when out of the screen
    private void OnBecameInvisible()
    {
        this.Destroy();
    }

    // Set particle damages
    private void SetDamages()
    {
        this.mDamages = this.fromPlayer ? Random.Range(3, 10) : Random.Range(1, 4);

    }

    // Set particle speed
    protected void SetSpeed()
    {
        this.mSpeed = this.fromPlayer ? 12f : 3f;
    }

    // Set the target position
    protected void SetTargetPosition(Vector3 targetPosition)
    {
        this.mTargetPosition = targetPosition;
    }

    // Set the moving direction of the particle
    protected void SetMoveDir()
    {
        this.particleMoveDir = (this.mTargetPosition - this.transform.position).normalized;
    }

    // Handle particle movements
    public void HandleMovements()
    {
        float distance = Vector3.Distance(this.mTargetPosition, this.transform.position);

        Vector3 newParticlePosition = this.transform.position + this.particleMoveDir * this.mSpeed * Time.deltaTime;
        newParticlePosition.z = 0f;

        this.transform.position = newParticlePosition;
    }

    // Handle when the particle enters in collision with the enemy
    public virtual void HandleCollisions()
    {
        // Check if the particle position points a target (by default the player)
        Entity target = Player.IsPlayerAt(this.transform.position);

        // If the particle was shot by the player, change the target to look for
        if (this.fromPlayer)
        {
            target = Enemy.IsEnemyAt(this.transform.position);
        }

        // If a target has been detected
        if (target)
        {
            bool isCritical = false;
            Utils.ApplyCritical(ref this.mDamages, ref isCritical);
            target.Damage(this.mDamages, isCritical);
            this.Destroy();
        }
    }

    // Destroy the particle
    public void Destroy()
    {
        // Remove the current particle instance from the particles list
        Particle.RemoveParticle(this);

        // Destroy the game object
        Destroy(gameObject);
    }

    // Return the current position of the particle
    public Vector3 GetCurrentPosition()
    {
        return this.transform.position;
    }

    // Return the entity taht shot the particle
    public Entity GetHostEntity()
    {
        return this.mHostEntity;
    }

    // Return if the particle was shot by the player
    public bool IsFromPlayer()
    {
        return this.fromPlayer;
    }

    // Set up particle prefab
    private static void SetupPrefab(Entity hostEntity)
    {
        if (hostEntity.GetType() == GameAssets.mInstance.mPlayer.GetType())
        {
            mPrefab = GameAssets.mInstance.GetParticle(PARTICLE_BASIC);

            return;
        }

        mPrefab = GameAssets.mInstance.GetParticle(PARTICLE_BASIC_ENEMY, hostEntity);
    }

    // Create a new instance of particle
    public static Particle Create(Vector3 targetPosition, Entity hostEntity)
    {
        SetupPrefab(hostEntity);

        Vector3 spawnPosition = Utils.GetShootPosition(hostEntity);

        Transform particleTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        Particle particle = particleTransform.GetComponent<Particle>();
        particle.Setup(targetPosition, hostEntity);

        Particle.RecordParticle(particle);

        return particle;
    }

    // Record a new particle instance
    protected static void RecordParticle(Particle particle)
    {
        // When an enemy is instantiated add it to the list of enemies
        particleList.Add(particle);
    }

    // Remove the particle from the particle list
    protected static void RemoveParticle(Particle particle)
    {
        particleList.Remove(particle);
    }

    // Check if there is a particle at the target position
    // Return the targeted particle instance if found
    public static Particle IsParticleAt(Vector3 targetPosition, Entity hostEntity)
    {
        Vector3 newTargetPosition = targetPosition;

        float maxRange;

        foreach (Particle particle in particleList)
        {
            if (particle.GetHostEntity().GetType() == GameAssets.mInstance.mPlayer.GetType()
                || hostEntity.GetType() == GameAssets.mInstance.mPlayer.GetType())
            {
                if (particle.GetHostEntity().GetType() != hostEntity.GetType())
                {
                    maxRange = Utils.GetSpriteSize(particle.gameObject).x / 2f;
                    Vector3 particlePosition = particle.GetCurrentPosition();
                    particlePosition.z = newTargetPosition.z = 0f;
                    float distance = Vector3.Distance(newTargetPosition, particlePosition);

                    if (distance <= maxRange)
                    {
                        return particle;
                    }
                }
            }
        }

        return null;
    }

    // Check if there is a particle in the provided range
    public static Particle IsParticleAt(Vector3 targetPosition, float maxRange, Entity hostEntity)
    {
        Vector3 newTargetPosition = targetPosition;

        foreach (Particle particle in particleList)
        {
            if (particle.GetHostEntity().GetType() == GameAssets.mInstance.mPlayer.GetType()
                || hostEntity.GetType() == GameAssets.mInstance.mPlayer.GetType())
            {
                if (particle.GetHostEntity().GetType() != hostEntity.GetType())
                {
                    Vector3 particlePosition = particle.GetCurrentPosition();
                    particlePosition.z = newTargetPosition.z = 0f;
                    float distance = Vector3.Distance(newTargetPosition, particlePosition);

                    if (distance <= maxRange)
                    {
                        return particle;
                    }
                }
            }                      
        }

        return null;
    }
}
