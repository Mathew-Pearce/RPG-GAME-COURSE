using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;

public EnemyStateMachine stateMachine { get; private set; }


    protected override void Awake()
    {
        stateMachine = new EnemyStateMachine ();
    }

    protected override void Update()
    {
        stateMachine.currentState.Update ();
    }
}
