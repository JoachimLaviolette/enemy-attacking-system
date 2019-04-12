using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExplosion : Particle
{
    private float timer;

    // Called every frame to update the explosion model
    protected override void Update()
    {
        base.Update(); 
    }

    // Set up explosion properties
    private void Start()
    {
        this.mDamages = Random.Range(10, 25);
    }

    protected override void HandleMovements() { }

    // Damage all the enemies nearby
    protected override void HandleCollisions()
    {
        // Check if there is any enemy nearby
        Enemy enemy = Enemy.IsEnemyAt(transform.position);

        if (enemy)
        {
            bool isCritical = false;
            Utils.ApplyCritical(ref this.mDamages, ref isCritical);
            DamageNotification.Create(enemy.GetCurrentPosition(), this.mDamages, isCritical);
            enemy.Damage(this.mDamages);
        }
    }

    // Check if the explosion has to be destroyed
    protected override void HandleDestroy()
    {
        if (this.gameObject.GetComponent<SpriteAnimatorExplosion>().ToDestroy())
        {
            this.Destroy();
        }
    }

    // Set up particle canon prefab
    private static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetParticle(PARTICLE_EXPLOSION);
    }

    // Create a new explosion instance
    public new static ParticleExplosion Create(Vector3 spawnPosition)
    {
        SetupPrefab();

        Transform particleTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);
        ParticleExplosion particle = particleTransform.GetComponent<ParticleExplosion>();
        Particle.RecordParticle(particle);

        return particle;
    }
}
