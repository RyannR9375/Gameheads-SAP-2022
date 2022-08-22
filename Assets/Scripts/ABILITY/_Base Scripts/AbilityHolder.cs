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
    #region Calling Scripts
    public Ability ability;
    private Player playerScript;
    [HideInInspector] public ShineAbility shineAbility;
    #endregion

    #region Declaring Variables
    float activeTime;
    float cooldownTime;
    private GameObject playerObject;
    private ParticleSystem releaseVFX;
    private ParticleSystem enemyParticles;
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
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<Player>();
        releaseVFX = GameObject.Find("Releaseparticles").GetComponent<ParticleSystem>();

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
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();

        #region Shine Ability
        if (enemy.gameObject.CompareTag("Enemy") && CompareTag("Shine") && playerScript.currentCharge >= playerScript.maxCharge)
        {
            ShineTrigger(enemy);
        }
        #endregion

        #region Release Ability
        if (enemy.gameObject.CompareTag("Enemy") && CompareTag("Release"))
        {
            Release(enemy);
        }
        #endregion
    }

    private void OnTriggerStay2D(Collider2D enemy)
    {
        #region Absorb Ability

        if (enemy.gameObject.CompareTag("Enemy") && CompareTag("Absorb"))
        {
            #region Enemy VFX
            if (enemy.gameObject.GetComponent<ParticleSystem>() != null)
            {
                enemyParticles = enemy.gameObject.GetComponent<ParticleSystem>();
            }

            if (enemyParticles != null)
            {
                var main = enemyParticles.main;
                main.duration = ability.activeTime;
                enemyParticles.Play();
            }
            #endregion

            Absorb(enemy);
        }
        else
        {
            if(enemyParticles != null)
            {
                enemyParticles.Stop();
            }

        }
        #endregion
    }

    private void OnCollisionEnter2D(Collision2D enemy)
    {
        #region Shine Ability
        Rigidbody2D enemyRB = enemy.gameObject.GetComponent<Rigidbody2D>();
        if (enemy.gameObject.CompareTag("Enemy") && CompareTag("Shine"))
        {
            ShineCollision(enemy);
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

    private IEnumerator ChargeDeplete()
    {
        yield return new WaitForSeconds(shineAbility.activeTime);
        playerScript.currentCharge = 0f;
    }
    #endregion

    #region Functions
    void Absorb(Collider2D enemy)
    {

        tag = "Absorb";
        scaleChange = new Vector3(-0.003f, -0.003f, 0f);
        minScale = new Vector3(0.2f, 1.077f, 0f);
        minScaleMag = (minScale.magnitude);

        //Makes sure the player isn't absorbing past the maximum
        if (playerScript.currentCharge < playerScript.maxCharge)
        {
            playerScript.absorbAdd(playerScript.chargeAmount);
        }

        //Checks if the enemy has hit the minimum scale, if not, keep decreasing their size
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
                Destroy(enemy.gameObject);
                //playing enemy death anim and disabling its movement? }
            }
        }

    }

    void Release(Collider2D enemy)
    {
        tag = "Release";
        Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();
        if (enemyRB != null)
        {
            Debug.Log("Calling Release AbilityPhysics");
            Destroy(enemy.gameObject);
        }

    }

    //RENAME TO RELEASE
    void ShineTrigger(Collider2D enemy)
    {
        Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();
        if (enemyRB != null && shineAbility != null)
        {
            #region Player VFX
            var main = releaseVFX.main;
            main.duration = shineAbility.activeTime;
            releaseVFX.Play();
            #endregion

            //PUSH FORCE MATH. '.FORCE' FELT BETTER THAN '.IMPULSE'
            //GETTING THE DISTANCE FROM THE ENEMY TO THE PLAYER, AND ADDING FORCE IN THE OPPOSITE VECTOR DIRECTION. 
            Vector2 difference = (enemy.transform.position - transform.position);
            difference = (difference.normalized * shineAbility.shinePush);
            enemyRB.AddForce(difference, ForceMode2D.Force);

            playerScript.TakeDamage(0f, 15f);
            StartCoroutine(KnockTime(enemyRB));

            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(shineAbility.damage);
            }

            StartCoroutine(ChargeDeplete());
            Debug.Log("Calling Shine AbilityPhysics");
        }
    }

    void ShineCollision(Collision2D enemy)
    {
        Rigidbody2D enemyRB = enemy.gameObject.GetComponent<Rigidbody2D>();
        if (enemy.gameObject.CompareTag("Enemy") && CompareTag("Shine"))
        {
            if (enemyRB != null && shineAbility != null && playerScript.currentCharge >= playerScript.maxCharge)
            {
                #region Player VFX
                var main = releaseVFX.main;
                main.duration = shineAbility.activeTime;
                releaseVFX.Play();
                #endregion

                //PUSH FORCE MATH. '.FORCE' FELT BETTER THAN '.IMPULSE'
                //GETTING THE DISTANCE FROM THE ENEMY TO THE PLAYER, AND ADDING FORCE IN THE OPPOSITE VECTOR DIRECTION. 
                Vector2 difference = (enemy.transform.position - transform.position);
                difference = (difference.normalized * shineAbility.shinePush);
                enemyRB.AddForce(difference, ForceMode2D.Force);

                playerScript.TakeDamage(0f, 15f);
                StartCoroutine(KnockTime(enemyRB));

                EnemyHealth enemyHealth = enemyRB.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                enemyHealth.Damage(shineAbility.damage);
                }

                StartCoroutine(ChargeDeplete());
                Debug.Log("Calling Shine AbilityPhysics");
            }
        }
    }
    #endregion
}
