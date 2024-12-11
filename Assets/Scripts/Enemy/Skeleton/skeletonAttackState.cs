using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonAttackState : enemyState
{
    // Start is called before the first frame update
    private enemySkeleton enemy;
    public skeletonAttackState(enemy _enemyBase, enemyStateMachine stateMachine, string animBoolName,enemySkeleton _enemy) : base(_enemyBase, stateMachine, animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if(triggerCalled)
        {
            stateMachine.change(enemy.battleState);
        }
    }
}
