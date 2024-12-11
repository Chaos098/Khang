using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyState 
{
    // Start is called before the first frame update
    public enemy enemyBase;
    public enemyStateMachine stateMachine;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    private string AnimBoolName;
    protected float stateTimer;

    public enemyState()
    {
    }

    public enemyState(enemy _enemyBase, enemyStateMachine stateMachine, string animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = stateMachine;
        AnimBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.anim.SetBool(AnimBoolName,true);
        rb = enemyBase.rb;
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(AnimBoolName,false);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void AnimationFinishTrigeer()
    {
        triggerCalled=true;
    }
}
