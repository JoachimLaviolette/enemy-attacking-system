using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRed : AI, IMovable, IShootable, IDefendable
{
    private float shieldTimer = 0f;

    public AIRed(Enemy enemy) : base(enemy) { }

    public override void Update()
    {
        this.HandleMovements();
        this.HandleShooting();
        this.HandleDefense();
    }

    public void HandleMovements()
    {
        Vector3 playerPosition = this.mPlayer.GetCurrentPosition();
        playerPosition.z = this.mEnemy.transform.position.z;
        Vector3 enemyPosition = playerPosition;
        Vector3 enemyMoveDir = (playerPosition - this.mEnemy.transform.position).normalized;
        float distance = Vector3.Distance(playerPosition, this.mEnemy.transform.position);

        if (distance > 0f)
        {
            Vector3 newEnnemyPosition = this.mEnemy.transform.position + enemyMoveDir * this.mEnemy.GetSpeed() * Time.deltaTime;
            float distanceAfterMoving = Vector3.Distance(newEnnemyPosition, enemyPosition);

            if (distanceAfterMoving > distance)
            {
                // Overshot the target
                newEnnemyPosition = enemyPosition;
            }

            newEnnemyPosition.z = 0f;
            this.CheckNewPosition(ref newEnnemyPosition);
            this.mEnemy.transform.position = newEnnemyPosition;

            return;
        }
    }

    // Handle AI's shooting actions
    public void HandleShooting()
    {
        // Launch one basic particle
        this.LaunchParticle(Particle.PARTICLE_BASIC);
    }

    // Launch a particle in the direction of the player
    public void LaunchParticle(int particleType)
    {
        // In 2% of the cases, the AI launches particles
        if (Random.Range(0, 100) < 2)
        {
            Vector3 playerPosition = this.mPlayer.GetCurrentPosition();

            switch (particleType)
            {
                case Particle.PARTICLE_BASIC:
                    Particle.Create(playerPosition, this.mEnemy);

                    return;
            }
        }
    }

    // Handle AI's defense actions
    public void HandleDefense()
    {
        // In 85% of the cases, the AI turns on enemy's shield
        if (Random.Range(0, 100) < 85)
        {
            if (Particle.IsParticleAt(this.mEnemy.GetCurrentPosition(), .5f, this.mEnemy))
            {
                ((EnemyRed)this.mEnemy).TurnOnShield();
            }
        }
    }

    // Adjust the position of the enemy if about to collapse a particle
    protected override void CheckNewPosition(ref Vector3 newPosition)
    {
        // In 30% of the cases, we try to make the enemy dodge
        if (Random.Range(0, 100) < 30)
        {
            float factor;

            if(Particle.IsParticleAt(newPosition, .5f, this.mEnemy))
            {
                if (Random.Range(0, 100) < 30)
                {
                    return;
                }

                // The direction the enemy dodges towards is random
                factor = -.1f;

                if (Random.Range(0, 100) > 75)
                {
                    factor = Mathf.Abs(factor);
                }

                newPosition += new Vector3(factor, factor);                     
            }
        }
    }
}
