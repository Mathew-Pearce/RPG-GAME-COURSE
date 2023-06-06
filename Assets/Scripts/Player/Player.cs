using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;

    public bool isBusy {  get; private set; }

    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;

    [Header("Dash info")]
    [SerializeField] private float dashCoolDown;
    private float dashhUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }


    public bool isBusyJumping = false;




    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
 
    #endregion


    protected override void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this,stateMachine, "Idle" );

        moveState = new PlayerMoveState(this, stateMachine, "Move");

        jumpState = new PlayerJumpState(this, stateMachine, "Jump");

        airState = new PlayerAirState(this, stateMachine, "Jump");

        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");

        dashState = new PlayerDashState(this, stateMachine, "Dash");

        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialise(idleState);
    }

    protected override void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    public IEnumerator BusyFor(float _seconds)
    {    
        isBusy = true;

        yield return new WaitForSeconds(_seconds );

        isBusy = false;
    }
    public IEnumerator BusyJumping(float _jumpTime)
    {
        isBusyJumping = true;

        yield return new WaitForSeconds(_jumpTime);

        isBusyJumping = false;
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinnishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;
        dashhUsageTimer -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.LeftShift) && dashhUsageTimer < 0)
        {
            dashhUsageTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
            {
                dashDir = facingDir;
            }

            stateMachine.ChangeState(dashState);
        }
            
    }



 
}
