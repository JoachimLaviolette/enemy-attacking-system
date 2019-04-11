using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCanon : Particle
{
    // Set up particle canon properties
    private void Start()
    {
        this.mSpeed = 8f;
        this.mDamages = 45f;
    }

    // Set up particle canon prefab
    private static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetParticle(PARTICLE_CANON);
    }

    // Create a new instance of particle
    public new static ParticleCanon Create(Vector3 targetPosition)
    {
        SetupPrefab();

        Vector3 spawnPosition = Utils.GetSpriteSize(GameAssets.mInstance.GetPlayer().gameObject);
        spawnPosition.x = 0f;
        spawnPosition.y /= 2f;
        spawnPosition.z = 0;
        spawnPosition += GameAssets.mInstance.GetPlayer().GetCurrentPosition();

        Transform particleTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);
        ParticleCanon particle = particleTransform.GetComponent<ParticleCanon>();
        particle.SetTargetPosition(targetPosition);
        particle.SetMoveDir();
        Particle.RecordParticle(particle);

        return particle;
    }
}
