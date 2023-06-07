using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float timeSinceLastAttack;
    private float comboWindow = 2;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time >= timeSinceLastAttack + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);

        player.SetVelocity(player.attackMovement[comboCounter].x * player.facingDir, player.attackMovement[comboCounter].y);

        stateTimer = .1f;


    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        timeSinceLastAttack = Time.time;
        player.StartCoroutine("BusyFor", .15f);
        
    }

    public override void Update()
    {
        base.Update();

        if (player.isBusy && xInput != 0)
        {
            if (player.facingDir != xInput)
            {
                player.Flip();
            }
        }

        if (stateTimer < 0)
            player.SetZeroVelocity();
       
        if(triggerCalled) 
            stateMachine.ChangeState(player.idleState);

       
    }
}
