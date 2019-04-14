using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRed : AI, IMovable, IShootable, IDefendable
{
    public AIRed(Enemy enemy) : base(enemy) { }
    public override void Update()
    {
        this.HandleMovements();
        this.HandleShooting();
        this.HandleDefense();
    }

    public void HandleMovements()
    {
        Vector3 playerPosition = mPlayer.GetCurrentPosition();
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

    public void HandleShooting()
    {
        
    }

    public void HandleDefense()
    {
        
    }

    // Adjust the position of the enemy if about to collapse a particle
    protected override void CheckNewPosition(ref Vector3 newPosition)
    {
        // In 30% of the cases, we try to make the enemy dodge
        if (Random.Range(0, 100) < 20)
        {
            float factor;

            while (Particle.IsParticleAt(newPosition, .5f))
            {
                if (Random.Range(0, 2) > 0)
                {
                    return;
                }

                // Dodge again in 2/3 of the cases
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
