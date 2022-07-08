using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// READS INPUT FROM PLAYERSTATE
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        //MAKES IT SO THAT THE MOVEMENT INPUT EITHER RETURNS 0-1. ONLY REALLY NEEDED IF YOU DON'T WANT CONTROLLER PLAYERS MOVING AT DIFFERENT SPEEDS THAN KB-M.
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnShineInput(InputAction.CallbackContext context)
    {
        


    }
}
