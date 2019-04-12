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
    public Particle pf_Particle, pf_ParticleCanon, pf_ParticleExplosion;
    public DamageNotification pf_DamageNotification;
    public Sprite[] sprite_Player, sprite_Health, sprite_Armor, sprite_ParticleCanon;

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

    public Particle GetParticle(int particleType)
    {
        switch (particleType)
        {
            case Particle.PARTICLE_CANON:
                return this.pf_ParticleCanon;
            case Particle.PARTICLE_EXPLOSION:
                return this.pf_ParticleExplosion;
            default:
                return this.pf_Particle;
        }
    }

    public Sprite[] GetPlayerSprites()
    {
        return this.sprite_Player;
    }

    public Sprite GetPlayerSprite(int playerState)
    {
        return this.sprite_Player[playerState];
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
}
