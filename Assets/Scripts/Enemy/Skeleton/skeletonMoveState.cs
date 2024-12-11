using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonMoveState : skeletonGroundState
{
    public skeletonMoveState(enemy _enemyBase, enemyStateMachine stateMachine, string animBoolName, enemySkeleton _enemy) : base(_enemyBase, stateMachine, animBoolName, _enemy)
    {
    }

    // Start is called before the first frame update


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir,rb.velocity.y);

        if(!enemy.IsGroundDetected()||enemy.IsWallDetected()) {
            enemy.Flip();
            stateMachine.change(enemy.enemyIdleState);
        }
    }
}
