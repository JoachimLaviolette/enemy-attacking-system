using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCanon : Particle
{
    // Set up particle canon properties
    private void Start()
    {
        this.mSpeed = 8f;
        this.mDamages = 45;
    }

    // Set up particle canon prefab
    private static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetParticle(PARTICLE_CANON);
    }

    // Create a new instance of particle
    public new static ParticleCanon Create(Vector3 targetPosition, Entity hostEntity)
    {
        SetupPrefab();

        Vector3 spawnPosition = Utils.GetShootPosition(hostEntity);
        Transform particleTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);
        ParticleCanon particle = particleTransform.GetComponent<ParticleCanon>();
        particle.Setup(targetPosition, hostEntity);
        Particle.RecordParticle(particle);

        return particle;
    }
}
