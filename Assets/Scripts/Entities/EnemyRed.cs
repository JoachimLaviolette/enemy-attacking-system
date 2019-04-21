using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : Enemy
{
    private SpriteAnimatorEnemyShield spriteAnimatorEnemyShield;
    private bool isShieldActivated;

    private void Awake()
    {
        this.spriteAnimatorEnemyShield = this.GetComponent<SpriteAnimatorEnemyShield>();
        this.spriteAnimatorEnemyShield.SetEnemy(this);
    }

    // Set up enemy red properties
    protected void Start()
    {
        this.mHealth = 30f;
        this.mSpeed = 1.5f;
        this.mDamages = 10;
        this.mReward = 20;
        this.mAI = new AIRed(this);
        this.isShieldActivated = false;
    }

    // Damage the enemy

    public override void Damage(int damages, bool isCritical)
    {
        if (!this.isShieldActivated)
        {
            base.Damage(damages, isCritical);
        }
    }

    // Set up enemy red prefab
    protected new static void SetupPrefab()
    {
        mPrefab = GameAssets.mInstance.GetEnemyRed();
    }

    // Create a new instance of enemy red
    public static EnemyRed Create(Vector3 spawnPosition)
    {
        Player player = GameAssets.mInstance.GetPlayer();

        if (!player.IsAlive())
        {
            return null;
        }

        SetupPrefab();

        Transform enemyTransform = Instantiate(mPrefab.transform, spawnPosition, Quaternion.identity);

        EnemyRed enemy = enemyTransform.GetComponent<EnemyRed>();
        enemy.Setup(() => player.GetCurrentPosition());

        Enemy.RecordEnemy(enemy);

        return enemy;
    }

    // Turn on the enemy's shield
    public void TurnOnShield()
    {
        this.isShieldActivated = true;

        // Play turned-on shield animation
        this.PlayTurnedOnShieldAnimation();

    }

    // Turn off the enemy's shield
    public void TurnOffShield()
    {
        this.isShieldActivated = false;
    }

    // Play the animation when the enemy's shield is activated
    private void PlayTurnedOnShieldAnimation()
    {
        spriteAnimatorEnemyShield.PlayAnimation();
    }
}
