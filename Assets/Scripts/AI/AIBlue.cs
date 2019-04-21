using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBlue : AI, IMovable
{
    public AIBlue(Enemy enemy) : base(enemy) { }

    public override void Update()
    {
        this.HandleMovements();
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

    // We never make an Enemy blue instance dodge
    protected override void CheckNewPosition(ref Vector3 newPosition)
    {
        return;
    }
}
