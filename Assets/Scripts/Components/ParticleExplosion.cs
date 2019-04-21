﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleExplosion : Particle
{
    private float timer;

    // Called every frame to update the explosion model
    protected override void Update()
    {
        base.Update();
        this.HandleDestroy();
    }

    // Set up explosion properties
    private void Start()
    {
        this.mDamages = Random.Range(10, 25);
    }

    // Damage all the enemies nearby
    public override void HandleCollisions()
    {
        // Check if there is any enemy nearby
        Enemy enemy = Enemy.IsEnemyAt(transform.position);

        if (enemy)
        {
            bool isCritical = false;
            Utils.ApplyCritical(ref this.mDamages, ref isCritical);
            DamageNotification.Create(enemy.GetCurrentPosition(), this.mDamages, isCritical);
            enemy.Damage(this.mDamages, isCritical);
        }
    }

    // Check if the explosion needs to be destroyed
    protected void HandleDestroy()
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
    public static ParticleExplosion Create(Vector3 spawnPosition)
    {
        SetupPrefab();

        Transform particleTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        ParticleExplosion particle = particleTransform.GetComponent<ParticleExplosion>();
        Particle.RecordParticle(particle);

        return particle;
    }
}
