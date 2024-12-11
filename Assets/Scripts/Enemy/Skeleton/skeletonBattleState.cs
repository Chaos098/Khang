using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class skeletonBattleState : enemyState
{
    // Start is called before the first frame update
    private enemySkeleton enemy;
    private Transform player;
    private int moveDir;
    public skeletonBattleState(enemy _enemyBase, enemyStateMachine stateMachine, string animBoolName,enemySkeleton _enemy) : base(_enemyBase, stateMachine, animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if(enemy.isPlayerDetected().distance < enemy.attackDistance)
            {
                if(canAttack())
                    stateMachine.change(enemy.attackState);
            }
        }
        else if (stateTimer < 0||Vector2.Distance(player.transform.position,enemy.transform.position)>=10)
        {
            stateMachine.change(enemy.enemyIdleState);
        }

        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }
    public bool canAttack()
    {
        if (Time.time > enemy.lastTimeAttack + enemy.cooldownAttack)
        {
            return true;
        }
        return false;
    }
}
