using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonIdleState : skeletonGroundState
{
    // Start is called before the first frame update

    public skeletonIdleState(enemy _enemyBase, enemyStateMachine stateMachine, string animBoolName, enemySkeleton _enemy) : base(_enemyBase, stateMachine, animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            stateMachine.change(enemy.moveState);
        }
    }
}
