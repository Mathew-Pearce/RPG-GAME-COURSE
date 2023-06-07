using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
 

    [Header("Attack Info")]
    [SerializeField] public float attackDistance;
    public float attackCoolDown;
    [HideInInspector] public float timeSinceLastAttack;
    public float chaseRange;

    public EnemyStateMachine stateMachine { get; private set; }


    protected override void Awake()
    {
        stateMachine = new EnemyStateMachine ();
    }

    protected override void Update()
    {
        stateMachine.currentState.Update ();
    }

    public virtual void AnimationFinnishTrigger() => stateMachine.currentState.AnimationFinnishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }

    private IEnumerator WaitToFlip()
    {
        if (IsWallDetected())
        {
            Flip();
            yield break;
        }
        yield return new WaitForSeconds(idleTime);
        Flip();
    }
}
