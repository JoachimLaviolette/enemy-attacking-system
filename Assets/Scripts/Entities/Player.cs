using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{ 
    // Player score
    private int mScore;
    // Player armor
    private float mArmor;
    // Player sprite renderer
    private SpriteRenderer mSpriteRenderer;
    private bool isReloading;
    // Canon munitions
    private int mCanonMunitions;

    // Static
    private static float startTime_Canon = 1f;
    private static float reloadTime_Canon = 3f;

    // Const
    public static float TOTAL_HEALTH = 30f;
    public static float TOTAL_ARMOR = 50f;
    private static int MAX_PARTICLE_CANON_CAPACITY = 5;
    private const int PLAYER_IDLE = 0;
    private const int PLAYER_MOVING = 1;

    private void Awake()
    {
        this.mSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Called every frame to update player model
    private void Update()
    {
        this.HandleMovements();
        this.HandleActions();
    }

    // Set up player properties
    private void Start()
    {
        this.mHealth = TOTAL_HEALTH;
        this.mArmor = 0f;
        this.mSpeed = 4.5f;
        this.isReloading = false;

        this.InitializeParticleMunitions();
    }

    // Initialize player's particles munitions
    private void InitializeParticleMunitions()
    {
        this.mCanonMunitions = MAX_PARTICLE_CANON_CAPACITY;
    }

    // Handle input movements
    protected override void HandleMovements()
    { 
        float moveX = 0f;
        float moveY = 0f;

        // up
        if (Input.GetKey(KeyCode.Z))
        {
            moveY += 1f;
        }

        // down
        if (Input.GetKey(KeyCode.S))
        {
            moveY -= 1f;
        }

        // left
        if (Input.GetKey(KeyCode.Q))
        {
            moveX -= 1f;
        }

        // right
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }

        Vector3 moveDir = new Vector3(moveX, moveY).normalized;
        this.lastMoveDir = moveDir;
        transform.position += moveDir * this.mSpeed * Time.deltaTime;

        this.HandleAnimations(moveX == 0 && moveY == 0);
    }
    
    // Handle player animation
    private void HandleAnimations(bool isIdle)
    {
        if (!isIdle)
        {
            this.PlayMovingAnimation();
        }
        else
        {
            this.PlayIdleAnimation();
        }
    }

    // Play moving animation
    private void PlayMovingAnimation()
    {
        this.mSpriteRenderer.sprite = GameAssets.mInstance.GetPlayerSprite(PLAYER_MOVING);
    }

    // Play idle animation
    private void PlayIdleAnimation()
    {
        this.mSpriteRenderer.sprite = GameAssets.mInstance.GetPlayerSprite(PLAYER_IDLE);
    }

    // Handle player's actions
    private void HandleActions()
    {
        // If mouse's left button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Attack the pointed enemy
            // this.AttackEnemy();

            // Launch one basic particle
            this.LaunchParticle(Particle.PARTICLE_BASIC);
        }

        // If space bar is pressed
        // Try to launch a canon particle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Launch one canon particle
            this.LaunchParticle(Particle.PARTICLE_CANON);
        }

        // If R key is pressed
        // Reload particles munitions
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (this.mCanonMunitions < MAX_PARTICLE_CANON_CAPACITY
                & !this.isReloading)
            {
                this.ReloadMunitions();
            }
        }
    }

    // Damage the player
    // Override the super method to take in account the armor system
    public override void Damage(float damages)
    {
        // If the player has an armor
        // Damage his armor
        if (this.mArmor > 0f)
        {
            this.mArmor -= damages;

            return;
        }

        // Otherwise damage the player
        base.Damage(damages);
    }

    // Attack the pointed enemy (if any)
    private void AttackEnemy()
    {
        // Check if the mouse position points an enemy
        Enemy enemy = Enemy.IsEnemyAt(Utils.GetWorldPosition((Input.mousePosition)));

        if (enemy)
        {
            enemy.Damage(5f);
        }
    }

    // Launch a particle in the direction pointed by the mouse cursor
    private void LaunchParticle(int particleType)
    {
        Vector3 mouseWorldPosition = Utils.GetWorldPosition(Input.mousePosition);

        switch (particleType)
        {
            case Particle.PARTICLE_CANON:
                if (this.mCanonMunitions > 0)
                {
                    ParticleCanon.Create(mouseWorldPosition);
                    this.mCanonMunitions--;
                    this.CancelReloading();
                }

                return;
            default:
                Particle.Create(mouseWorldPosition);

                return;
        }
    }

    // Reload player's particles munitions
    private void ReloadMunitions()
    {
        InvokeRepeating("ReloadParticleCanon", startTime_Canon, reloadTime_Canon);
    }

    // Reload player's canon munitions
    private void ReloadParticleCanon()
    {
        if (this.mCanonMunitions < MAX_PARTICLE_CANON_CAPACITY)
        {
            this.isReloading = true;
            this.mCanonMunitions++;

            return;
        }

        this.CancelReloading();
    }

    // Cancel current reloading
    private void CancelReloading()
    { 
        this.isReloading = false;
        CancelInvoke();
    }

    // Check if there is the player at the target position
    public static Player IsPlayerAt(Vector3 targetPosition)
    {
        Player player = GameAssets.mInstance.GetPlayer();
        Vector3 newTargetPosition = targetPosition;
        newTargetPosition.z = player.GetCurrentPosition().z;
        float maxRange = Utils.GetSpriteSize(player.gameObject).x / 2f;
        float distance = Vector3.Distance(player.GetCurrentPosition(), newTargetPosition);

        if (distance <= maxRange)
        {
            return player;
        }

        return null;
    }

    // Get the score of the player
    public int GetScore()
    {
        return this.mScore;
    }

    // Get the health of the player
    public float GetHealth()
    {
        return this.mHealth;
    }

    // Add a certain amount of health to the player
    public void AddHealth(float health)
    {
        this.mHealth += health;

        if (this.mHealth > TOTAL_HEALTH)
        {
            this.mHealth = TOTAL_HEALTH;
        }
    }

    // Get the armor of the player
    public float GetArmor()
    {
        return this.mArmor;
    }

    // Add a certain amount of armor to the player
    public void AddArmor(float armor)
    {
        this.mArmor += armor;

        if (this.mArmor > TOTAL_ARMOR)
        {
            this.mArmor = TOTAL_ARMOR;
        }
    }

    // Get the amount of particle canon munitions of the player
    public int GetParticleCanonMunitions()
    {
        return this.mCanonMunitions;
    }

    // Set the score of the player
    public void SetScore(int score)
    {
        this.mScore = score;
    }
}
