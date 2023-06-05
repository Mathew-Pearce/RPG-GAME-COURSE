using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerIdleState
{
    private int comboCounter;

    private float timeSinceLastAttack;
    private float comboWindow = 2;

    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(comboCounter > 2 || Time.time >= timeSinceLastAttack + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        timeSinceLastAttack = Time.time;

    }

    public override void Update()
    {
        base.Update();
        if(triggerCalled) 
            stateMachine.ChangeState(player.idleState);
    }
}
