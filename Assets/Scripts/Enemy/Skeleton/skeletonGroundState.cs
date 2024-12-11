using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonGroundState : enemyState
{
    // Start is called before the first frame update
    protected enemySkeleton enemy;
    private Transform player;
    public skeletonGroundState(enemy _enemyBase, enemyStateMachine stateMachine, string animBoolName,enemySkeleton _enemy) : base(_enemyBase, stateMachine, animBoolName)
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
        if (enemy.isPlayerDetected()||Vector2.Distance(enemy.transform.position,player.transform.position)<=2)
        {
            stateMachine.change(enemy.battleState);
        }
    }
}
