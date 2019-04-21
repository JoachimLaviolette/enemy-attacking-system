using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGreen : AI, IMovable, IShootable
{
    public AIGreen(Enemy enemy) : base(enemy) { }

    public override void Update()
    {
        this.HandleMovements();
        this.HandleShooting();
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
        if (Random.Range(0f, 100f) < .5f)
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

    // Adjust the position of the enemy if about to collapse a particle
    protected override void CheckNewPosition(ref Vector3 newPosition)
    {
        // In 5% of the cases, we try to make the enemy dodge
        if (Random.Range(0, 100) < 5)
        {
            float factor;

            while (Particle.IsParticleAt(newPosition, .5f, this.mEnemy))
            {
                if (Random.Range(0, 2) > 0)
                {
                    return;
                }

                // Dodge again in 1/3 of the cases
                factor = -.1f;

                if (Random.Range(0, 1) > 0)
                {
                    factor = Mathf.Abs(factor);
                }

                newPosition += new Vector3(factor, factor);
            }
        }
    }
}
