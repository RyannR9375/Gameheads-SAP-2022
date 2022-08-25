using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //FOG OF WAR ?
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveStateX { get; private set; }
    public PlayerMoveState MoveStateY { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerAbleToMoveState playerAbleToMove { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components
    public Rigidbody2D rb { get; private set; }
    public HealthBar healthBar;
    public AbsorbBar absorbBar;
    public Transform respawnPoint;
    private Collider2D triggerCollider;
    private SpriteRenderer playerSprite;
    private GameObject abilityHolder;
    private GameObject attackArea = default;

    public Vector2 CurrentVelocity;
    private Vector2 workspace;
    #endregion

    #region Other Variables
    //PLAYER ROTATION AND COLLISION STUFF
    public float FacingDirection { get; private set; }
    public float knockTime;

    //ALL ATTACK AND BLOCK STUFF
    private bool attacking = false;
    private bool blocking = false;

    private float timerAtk = 0f;
    private float timerBlock = 0f;
    private float rotationOffset;

    #endregion

    #region Player Stats

    [Header("Player Stats")]
    public int lives;
    public float maxHealth;
    public float currentHealth;
    private bool canTakeDamage = true;

    [Header("Ability Meter")]
    //ABILITY BAR
    public float maxCharge;
    public float currentCharge;
    public float chargeAmount;

    [Header("Attack and Block")]
    public float attackDamage = 10f;
    public float timeToAttack = 0.25f;
    public float timeToBlock = 1f;

    [Header("IFrame")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
        MoveStateX = new PlayerMoveState(this, StateMachine, playerData, "MoveHorizontal");
        MoveStateY = new PlayerMoveState(this, StateMachine, playerData, "MoveVertical");

    }

    private void Start()
    {
        //Grabs the Animator and the InputHandler from the gameobject this script is attached to
        InputHandler = GetComponent<PlayerInputHandler>();
        Anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        triggerCollider = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        abilityHolder = transform.GetChild(0).gameObject;
        attackArea = transform.GetChild(1).gameObject;


        //refactor to scriptable objects
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            currentHealth = maxHealth;
        }

        if (absorbBar != null)
        {
            absorbBar.SetMaxValue(maxCharge);
            currentCharge = 0f;
        }

        //FacingDirection = 1;

        //So that when game starts the first state that our player goes into is Idle
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        rb.velocity = CurrentVelocity;
        StateMachine.CurrentState.LogicUpdate();
        CheckStatus();

        //will move into statemachine, change functionality to work with designated buttons for controller support
        #region will move into statemachines
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetAxis("Attack") >= 1)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetAxis("Block") >= 1)
        {
            FastBlock();
        }

        if (attacking)
        {
            timerAtk += Time.deltaTime;

            if(timerAtk >= timeToAttack)
            {
                timerAtk = 0f;
                attacking = false;
                Anim.SetBool("Attacking", false);
                attackArea.SetActive(attacking);
            }
        }

        if (blocking)
        {
            timerBlock += Time.deltaTime;

            if(timerBlock >= timeToBlock)
            {
                timerBlock = 0f;
                blocking = false;
                canTakeDamage = true;
                Anim.SetBool("Blocking", false);
            }
        }
        #endregion
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        tag = "Player";
    }
    #endregion

    #region Collision Functions

    private void OnCollisionEnter2D(Collision2D other) // POTENTIALLY MOVING THIS TO IF THE ENEMY COLLIDES WITH THE PLAYER INSTEAD OF PLAYER COLLIDING WITH ENEMY, JUST TO KEEP PLAYER SCRIPTS CLEAN.
    {
        #region Player & Enemy 
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player") && canTakeDamage)
        {
            playerGetHit(5f);
        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player"))
        {
            //Damage?
        }

    }

    #endregion

    #region Set Functions
    public void SetVelocity(float x, float y)
    {
        workspace.Set(x, y);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion

    #region Other Functions

    //USED TO FLIP CHARACTER SPRITE, WON'T BE NEEDED SINCE WE LEANED INTO TOP-DOWN, BUT MAY BE USEFUL AS A TAKE DAMAGE EFFECT?
    private void Flip()
    {
        FacingDirection *= -1f;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
   
    //move into stateMachine
    void Attack()
    {

        Anim.SetBool("Attacking", true);
        attacking = true;
        attackArea.SetActive(attacking);

        Transform attackAreaTransform = attackArea.transform;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(attackAreaTransform.position);
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 0;
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        attackAreaTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));


    }

    void FastBlock()
    {
        Debug.Log("Fast Blocking");
        blocking = true;
        canTakeDamage = false;
    }

    //SIMPLE DAMAGE SCRIPT
    public void playerGetHit(float damage)
    {
        if(lives > 0 && currentHealth > 0 && canTakeDamage == true)
        {
            currentHealth -= damage;
        }
    }

    //TAKE DAMAGE AND GET KNOCKED BACK
    public void TakeDamage(float knockTime, float damage)
    {
        if(canTakeDamage == true)
        {
            currentHealth -= damage;
        }

        //Debug.Log($"you have {lives} Lives and {currentHealth} Health");
        if (lives > 0 && currentHealth > 0)
        {
            
            StartCoroutine(KnockCo(knockTime));
        }
    }

    public void CheckStatus()
    {
        if(healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (absorbBar != null)
        {
            absorbBar.SetValue(currentCharge);
        }

        if (lives > 0 && currentHealth <= 0)
        {
            transform.position = respawnPoint.position;
            currentHealth = maxHealth;
            lives -= 1;
        }
        else if (lives <= 0)
        {
            //change with defeat/ try agian
            gameObject.SetActive(false);
            GameManager.MyInstance.LoseScreen();
        }
    }

    //ADDS TO THE ABSORB METER WHEN PLAYER IS ABSORBING. NOT SURE HOW I FEEL ABOUT THIS DEPENDANCY.
    public void absorbAdd(float value)
    {
        currentCharge += value;

        if(absorbBar != null)
        {
            absorbBar.SetValue(currentCharge);
        }
    }
    #endregion

    #region Coroutines
    //STOPS CHARACTER MOVEMENT FOR A SECOND AFTER YOU GET HIT IN ORDER TO ACTIVATE IFRAMES AND APPLY KNOCKBACK
    private IEnumerator KnockCo(float knockTime)
    {
        if (rb != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            //rb.velocity = Vector2.zero;
            //PLAYER ANIM
            //rb.velocity = Vector2.zero;
        }
    }

    //CHARACTER FLASHING BECAUSE OF DAMAGE
    public IEnumerator FlashCo()
    {
        int temp = 0;
        canTakeDamage = false;
        
        while (temp < numberOfFlashes)
        {
            playerSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        canTakeDamage = true;
    }

    #endregion
}
