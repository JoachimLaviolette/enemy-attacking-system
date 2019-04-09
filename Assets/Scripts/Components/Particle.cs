﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    protected static Particle mPrefab;
    protected Vector3 mTargetPosition;
    protected float mDamages = 5f;
    protected float mSpeed = 4f;
    protected Vector3 particleMoveDir;
    protected static List<Particle> particleList = new List<Particle>();

    // Called every frame to update the particle model
    private void Update()
    {
        foreach (Particle p in particleList)
        {
            p.HandleMovements();
            p.HandleCollision();
        }
    }

    // Set the target position
    private void SetTargetPosition(Vector3 targetPosition)
    {
        this.mTargetPosition = targetPosition;
    }

    // Set the moving direction of the particle
    private void SetMoveDir()
    {
        this.particleMoveDir = (this.mTargetPosition - transform.position).normalized;
    }

    // Handle particle movement since it has been instantiated
    protected void HandleMovements()
    {
        Vector3 newParticlePosition = transform.position + this.particleMoveDir * this.mSpeed * Time.deltaTime;
        newParticlePosition.z = 0f;
        transform.position = newParticlePosition;
    }

    // Automatically destroy the particle when off the screen
    private void OnBecameInvisible()
    {
        this.Destroy();
    }

    // Handle when the particle enters in collision with the enemy
    protected void HandleCollision()
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
        // Remove the current particle instance from the particle list
        particleList.Remove(this);

        // Destroy the game object
        Destroy(gameObject);
    }

    // Set up particle prefab
    private static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetParticle();
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
