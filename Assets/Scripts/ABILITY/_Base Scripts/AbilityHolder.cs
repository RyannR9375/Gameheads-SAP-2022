using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
public class AbilityHolder: MonoBehaviour
{
    /// <summary>
    /// REFACTORING INTO CALLING 'ABILITY PHYSICS' PRIVATELY, AND 'ABILITY PHYSICS' SCRIPT CALLS THE SCRIPTABLE OBJECTS IN THE ARRAY 'AbilityList[]', 
    /// THAT WAY YOU DONT HAVE TO ADD ABILITYPHYSICS INTO THE INSPECTOR.
    /// </summary>

    #region Calling Scripts
    public Ability ability;
    private PlayerMovement player;
    [HideInInspector] public ShineAbility shineAbility;
    #endregion

    #region Declaring Variables
    float activeTime;
    float cooldownTime;
    private string objectName;
    private GameObject abilityHolder;
    private Vector3 scaleChange;
    private Vector3 minScale;
    private float minScaleMag;
    //AUTOMATICALLY SETTING ABILITY STATE TO TRUE
    [HideInInspector] public AbilityState state = AbilityState.ready;

    public enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    // WORK ON MAKING THIS AN INSPECTOR FRIENDLY THING WHERE YOU CAN CHOOSE THE AMOUNT OF ABILITIES THAT YOU WANT TO CALL.
    //private AbilityState debugState; //CHANGE TO PUBLIC WHEN TRYING TO DEBUG. ALLOWS YOU TO CYCLE BETWEEN 'READY', 'ACTIVE', AND 'COOLDOWN' THROUGH THE GUI.
    //ADDING INSPECTOR FRIENDLY KEY-BIND CHANGING, POTENTIALLY MAKING THIS PART OF THE ABILITYLIST SO YOU CAN DEFINE THE ABILITY TYPE AND THE KEY YOU WANT TO USE WITH IT IN ONE ELEMENT.
    public KeyCode key;
    #endregion

    #region Unity Callback Functions & GUI Stuff
    private void Awake()
    {
        scaleChange = new Vector3(-1f, -1f, -0f);
        abilityHolder = gameObject;
        objectName = gameObject.name;
    }

    //GUI, UNCOMMENT WHEN DEBUGGING.
    void OnGUI()
    {
        //debugState = (AbilityState)EditorGUILayout.EnumPopup("Pick an Ability State: ", debugState);
    }

    void Update()
    {
        //ClickForActivate();
        HoldForActivate();
    }
    #endregion

    #region Collision Functions
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();

        #region Shine Ability
        //SHINE ABILITY TRIGGER COLLISION. CHECKS FOR BOTH 'ENEMY' TAG AND 'SHINE' TAG.
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Shine"))
        {
            //CHECKING IF ENEMY HAS Rigidbody2D 
            if (enemy != null && shineAbility != null)
            {
                //PUSH FORCE MATH. '.FORCE' FELT BETTER THAN '.IMPULSE'
                //GETTING THE DISTANCE FROM THE ENEMY TO THE PLAYER, AND ADDING FORCE IN THE OPPOSITE VECTOR DIRECTION. 
                Vector2 difference = (enemy.transform.position - transform.position);
                difference = (difference.normalized * shineAbility.shinePush);
                enemy.AddForce(difference, ForceMode2D.Force);

                StartCoroutine(KnockTime(enemy));
                //Debug.Log("Calling Shine AbilityPhysics");
            }
        }
        #endregion

        #region Release Ability
        //RELEASE ABILITY TRIGGER COLLISION. CHECKS FOR BOTH 'ENEMY' TAG AND 'RELEASE' TAG.
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Release"))
        {
            //CHECKING IF ENEMY HAS Rigidbody2D 
            if (other != null)
            {
                //PUSH FORCE MATH. '.FORCE' FELT BETTER THAN '.IMPULSE'
                //Vector2 difference = (enemy.transform.position - transform.position);
                //difference = (difference.normalized * releaseAbility.releasePush);
                //enemy.AddForce(difference, ForceMode2D.Force);
                //StartCoroutine(KnockTime(enemy));
                Debug.Log("Calling Release AbilityPhysics");
                Destroy(other.gameObject);
            }
        }
        #endregion
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        #region Absorb Ability
        if (other.gameObject.CompareTag("Enemy") && CompareTag("Absorb"))
        {
            absorbRelease(other);
        }
        #endregion
    }

    #endregion

    #region ENUMS
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
    private IEnumerator KnockTime(Rigidbody2D enemy)
    {
        //MAKES SURE THE ENEMY NOT NULL & THAT THEIR VELOCITY DOESN'T = 0.
        if (enemy != null && enemy.velocity != Vector2.zero)
        {
            yield return new WaitForSeconds(1f);
            enemy.velocity = Vector2.zero;
        }
    }

    void absorbRelease(Collider2D enemy)
    {
        tag = "Absorb";
        scaleChange = new Vector3(-0.003f, -0.003f, 0f);
        minScale = new Vector3(0.2f, 1.077f, 0f);
        minScaleMag = (minScale.magnitude);

        if (enemy.transform.localScale.magnitude > minScaleMag)
        {
            enemy.transform.localScale += scaleChange;
        }
        else
        {
            //Can no longer absorb from enemies if too small. taking away enemy hit ability, and knockback features.
            //enemy.gameObject.tag = "Untagged";
            if (enemy != null)
            {
                GameObject enemyBody = enemy.gameObject;
                Destroy(enemyBody);
                //playing enemy death anim and disabling its movement? }
            }
        }

    }
    #endregion
}
