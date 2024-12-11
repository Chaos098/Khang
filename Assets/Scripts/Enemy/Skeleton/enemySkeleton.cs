using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class enemySkeleton : enemy
{
    #region State
    public skeletonIdleState enemyIdleState {  get; private set; }
    public skeletonMoveState moveState { get; private set; }
    public skeletonBattleState battleState { get; private set; }
    public skeletonAttackState attackState { get; private set; }
    public SkeletonDeadState deadState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        enemyIdleState = new skeletonIdleState(this,stateMachine,"Idle",this);
        moveState = new skeletonMoveState(this,stateMachine,"Move",this);
        battleState = new skeletonBattleState(this, stateMachine, "Move", this);
        attackState = new skeletonAttackState(this, stateMachine, "Attack", this);
        deadState = new SkeletonDeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.init(enemyIdleState);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override void Die()
    {
        base.Die();
        stateMachine.change(deadState);

    }
}
