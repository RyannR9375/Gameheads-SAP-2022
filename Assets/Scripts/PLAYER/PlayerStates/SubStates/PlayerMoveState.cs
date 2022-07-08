using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerAbleToMoveState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //CHECKING IF THE PLAYER CAN FLIP DIRECTIONS. BASED ON PLAYER SCRIPTS FACING DIRECTIONS
        player.CheckIfShouldFlip(input.x);

        player.SetVelocity(playerData.movementVelocity * input.x, playerData.movementVelocity * input.y);

        if(input.x == 0 && input.y == 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
