using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets mInstance;
    public Player mPlayer;
    public EnemyBlue pf_EnemyBlue;
    public EnemyGreen pf_EnemyGreen;
    public EnemyRed pf_EnemyRed;
    public ItemHealth pf_ItemHealth;
    public ItemArmor pf_ItemArmor;
    public Particle pf_Particle, pf_Particle_Enemy, pf_ParticleCanon, pf_ParticleExplosion;
    public DamageNotification pf_DamageNotification;
    public Sprite[] sprite_Health, sprite_Armor, sprite_ParticleCanon;
    [SerializeField] private Sprite[] sprite_Particle_Enemy;

    private const int PARTICLE_ENEMY_BLUE = 0;
    private const int PARTICLE_ENEMY_GREEN = 1;
    private const int PARTICLE_ENEMY_RED = 2;

    private void Awake()
    {
        mInstance = this;
    }

    public Player GetPlayer()
    {
        return this.mPlayer;
    }

    public Enemy[] GetEnemies()
    {
        Enemy[] enemies =
        {
            this.pf_EnemyBlue, this.pf_EnemyGreen, this.pf_EnemyRed
        };

        return enemies;
    }

    public EnemyBlue GetEnemyBlue()
    {
        return this.pf_EnemyBlue;
    }

    public EnemyGreen GetEnemyGreen()
    {
        return this.pf_EnemyGreen;
    }

    public EnemyRed GetEnemyRed()
    {
        return this.pf_EnemyRed;
    }

    public ItemHealth GetItemHealth()
    {
        return this.pf_ItemHealth;
    }

    public ItemArmor GetItemArmor()
    {
        return this.pf_ItemArmor;
    }

    public Particle GetParticle(int particleType, Entity hostEntity = null)
    {
        switch (particleType)
        {
            case Particle.PARTICLE_BASIC_ENEMY:
                return this.SetupParticle_Enemy(hostEntity);
            case Particle.PARTICLE_CANON:
                return this.pf_ParticleCanon;
            case Particle.PARTICLE_EXPLOSION:
                return this.pf_ParticleExplosion;
            default:
                return this.pf_Particle;
        }
    }

    public Sprite[] GetHealthSprites()
    {
        return this.sprite_Health;
    }

    public Sprite GetHealthSprite(int healthLevel)
    {
        return this.sprite_Health[healthLevel];
    }

    public Sprite[] GetArmorSprites()
    {
        return this.sprite_Armor;
    }

    public Sprite GetArmorSprite(int armorLevel)
    {
        return this.sprite_Armor[armorLevel];
    }

    public Sprite GetParticleCanonSprite(int particleCanonAmount)
    {
        return this.sprite_ParticleCanon[particleCanonAmount];
    }

    public DamageNotification GetDamageNotification()
    {
        return this.pf_DamageNotification;
    }

    private Sprite GetParticle_EnemySprite(int particleEnemyType)
    {
        return this.sprite_Particle_Enemy[particleEnemyType];
    }

    private Particle SetupParticle_Enemy(Entity hostEntity)
    {
        this.pf_Particle_Enemy.GetComponent<SpriteRenderer>().sprite = this.GetParticle_EnemySprite(PARTICLE_ENEMY_BLUE);

        if (hostEntity.GetType() == this.GetEnemyGreen().GetType())
        {
            this.pf_Particle_Enemy.GetComponent<SpriteRenderer>().sprite = this.GetParticle_EnemySprite(PARTICLE_ENEMY_GREEN);
        }

        if (hostEntity.GetType() == this.GetEnemyRed().GetType())
        {
            this.pf_Particle_Enemy.GetComponent<SpriteRenderer>().sprite = this.GetParticle_EnemySprite(PARTICLE_ENEMY_RED);
        }

        return this.pf_Particle_Enemy;
    }
}
