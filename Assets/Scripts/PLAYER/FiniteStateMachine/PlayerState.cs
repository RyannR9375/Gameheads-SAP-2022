using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// IS THE SCRIPT FOR DETERMINING WHETHER THE PLAYER IS MOVING, USING AN ABILITY, ETC.
/// </summary>
public class PlayerState
{
    //DECLARING VARIABLES
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        //SPECIFYING 'THIS' BECAUSE THEY HAVE THE SAME VARIABLE NAMES AS DECLARED.
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    //CALLED WHEN WE ENTER A STATE
    public virtual void Enter()
    {
        DoChecks();
        
        //grabbing the player animator, setting the bool of the current animation to true so it starts.
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;

        Debug.Log(animBoolName);
    }

    //CALLED WHEN WE EXIT A STATE
    public virtual void Exit()
    {
        //grabbing the player animator, setting the bool of the current animation to false so it stops.
        player.Anim.SetBool(animBoolName, false);
    }

    //REGULAR UPDATE FUNCTION
    public virtual void LogicUpdate()
    {

    }

    //FIXED UPDATE FUNCTION
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    //WILL CALL FROM ENTER AND PHYSICS UPDATE. CHECK FOR WALLS, THINGS LIKE THAT
    public virtual void DoChecks()
    {

    }

}
