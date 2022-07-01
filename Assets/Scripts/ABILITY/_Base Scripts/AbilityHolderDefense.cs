using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class AbilityHolderDefense : MonoBehaviour
{

/// <summary>
/// REFACTORING INTO CALLING 'ABILITY PHYSICS' PRIVATELY, AND 'ABILITY PHYSICS' SCRIPT CALLS THE SCRIPTABLE OBJECTS IN THE ARRAY 'AbilityList[]', THAT WAY YOU DONT HAVE TO ADD ABILITYPHYSICS INTO THE INSPECTOR.
/// </summary>

    //CALLING SCRIPTS
    public Ability ability;
    public PlayerMovement player;
    // WORK ON MAKING THIS AN INSPECTOR FRIENDLY THING WHERE YOU CAN CHOOSE THE AMOUNT OF ABILITIES THAT YOU WANT TO CALL.
    //public Ability[] AbilityList;
    private AbilityState debugState; //CHANGE TO PUBLIC WHEN TRYING TO DEBUG. ALLOWS YOU TO CYCLE BETWEEN 'READY', 'ACTIVE', AND 'COOLDOWN' THROUGH THE GUI.

    //GUI
    void OnGUI()
    {
        debugState = (AbilityState)EditorGUILayout.EnumPopup("Pick an Ability State: ", debugState);
    }

    //DECLARING VARIABLES
    float activeTime;
    float cooldownTime;
    //[HideInInspector] public bool activateStatus;

    //CREATING TYPES OF ABILITY STATES
    public enum AbilityState 
    {
        ready,
        active,
        cooldown
    }

    //AUTOMATICALLY SETTING ABILITY STATE TO TRUE
    [HideInInspector] public AbilityState state = AbilityState.ready;

    //ADDING INSPECTOR FRIENDLY KEY-BIND CHANGING, POTENTIALLY MAKING THIS PART OF THE ABILITYLIST SO YOU CAN DEFINE THE ABILITY TYPE AND THE KEY YOU WANT TO USE WITH IT IN ONE ELEMENT.
    public KeyCode key;
    //public KeyCode key2;

    void Update()
    {
        //ClickForActivate();
        HoldForActivate();
    }

    //DIFFERENT ABILITY SETUPS

    //SHINE SWITCH STATE
    public void ClickForActivate() 
    {
        switch (state)
        {
            //READY STATE
            case AbilityState.ready:
                if (Input.GetKeyDown(key))
                {
                    //WHEN ABILITY IS CALLED, ENSURE THAT THE SWITCH STATE IS SET TO ACTIVE
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    //SETTING THE ACTIVE TIME = TO THE TIME DEFINED IN THE ABILITY SCRIPTABLE OBJECT
                    activeTime = ability.activeTime;
                }
                break;

            //ACTIVE STATE
            case AbilityState.active:
                if (activeTime > 0)
                {
                    //IF ABILITY IS ACTIVE, SUBTRACT TIME UNTIL 0
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    //IF ACTIVE TIME IS LESS THAN 0, BEGIN AND CALL COOLDOWN
                    ability.BeginCooldown(gameObject);
                    state = AbilityState.cooldown;
                    //SETTING COOLDOWN TIME = TO A DECREASING TIME DEFINED IN THE 'COOLDOWN' SWITCH STATE
                    cooldownTime = ability.cooldownTime;
                }
                break;

            //COOLDOWN STATE
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    //IF THE ABILITY IS ON COOLDOWN, SUBTRACT TIME UNTIL 0
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    //WHEN THE COOLDOWN TIME IS 0, SET THE SWITCH STATE TO 'READY'
                    state = AbilityState.ready;
                }
                break;
        }
    }

    //ABSORB SWITCH STATE
    public void HoldForActivate()
    {
        switch (state)
        {
            //READY STATE
            case AbilityState.ready:
                if ((Input.GetKeyDown(key) && activeTime >= 0))
                {
                    //Debug.Log("Ready");
                    //WHEN ABILITY IS CALLED, ENSURE THAT THE SWITCH STATE IS SET TO ACTIVE
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    //SETTING THE ACTIVE TIME = TO A DECREASING TIME DEFINED IN THE 'ACTIVE' SWITCH STATE
                    activeTime = ability.activeTime;
                    activeTime -= Time.deltaTime;
                }
                break;

            //ACTIVE STATE
            case AbilityState.active:
                if ((Input.GetKeyUp(key) || activeTime < 0))//FIX
                {
                    //Debug.Log("Active");
                    //IF ACTIVE TIME IS LESS THAN 0, BEGIN AND CALL COOLDOWN
                    ability.BeginCooldown(gameObject);
                    state = AbilityState.cooldown;
                    //SETTING COOLDOWN TIME = TO A DECREASING TIME DEFINED IN THE 'COOLDOWN' SWITCH STATE
                    cooldownTime = ability.cooldownTime;
                }
                else
                {
                    //activateStatus = true;
                    activeTime -= Time.deltaTime;
                }
                
                break;

            //COOLDOWN STATE
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    //Debug.Log("CD");
                    //IF THE ABILITY IS ON COOLDOWN, SUBTRACT TIME UNTIL 0
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    //Debug.Log("CD Else");
                    //WHEN THE COOLDOWN TIME IS 0, SET THE SWITCH STATE TO 'READY' AND RESET 'ACTIVE TIME' ABILITY.
                    state = AbilityState.ready;
                    activeTime = ability.activeTime;
                }
                break;
        }
    }
}
