using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonStunState : enemyState
{
    private enemySkeleton enemy;
    public skeletonStunState(enemy _enemyBase, enemyStateMachine stateMachine, string animBoolName,enemySkeleton _enemy) : base(_enemyBase, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.stunTime;
        enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);
        rb.velocity = new Vector2(-enemy.facingDir*enemy.stunDirection.x,enemy.stunDirection.y);
    }

    public override void Exit()
    {
        enemy.fx.Invoke("CancelRedBlink", 0);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer<0)
        {
            stateMachine.change(enemy.enemyIdleState);
        }
    }
}
