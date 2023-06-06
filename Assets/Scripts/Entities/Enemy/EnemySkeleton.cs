using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region States
    public SkelletonIdleState idleState { get; private set; }
    public SkelletonMoveState moveState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkelletonIdleState(this, stateMachine, "Idle", this);
        moveState = new SkelletonMoveState(this, stateMachine, "Move", this);
    
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialise(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
