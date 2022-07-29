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
    public PlayerInputHandler InputHandler { get; private set; }

    [SerializeField] private PlayerData playerData;
    #endregion

    #region Components
    public Rigidbody2D rb { get; private set; }
    public HealthBar healthBar;
    public AbsorbBar absorbBar;
    private Collider2D triggerCollider;
    private SpriteRenderer playerSprite;
    private Transform abilityHolderTransform;
    private GameObject abilityHolder;

    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;
    #endregion

    #region Other Variables
    public float FacingDirection { get; private set; }
    public float Lives { get; private set;}

    #endregion

    #region Player Stats

    [Header("Player Stats")]
    public int maxHealth;
    public int currentHealth;

    [Header("Ability Meter")]
    //ABILITY BAR
    public float maxCharge;
    public float currentCharge;
    public float chargeAmount;

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
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        triggerCollider = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        abilityHolderTransform = gameObject.transform.GetChild(0);
        abilityHolder = abilityHolderTransform.gameObject;


        //refactor to scriptable objects
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;

        //FacingDirection = 1;

        //So that when game starts the first state that our player goes into is Idle
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        //CurrentVelocity = rb.velocity;
        rb.velocity = CurrentVelocity;
        StateMachine.CurrentState.LogicUpdate();
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
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player"))
        {
            //StartCoroutine(KnockCo(3f)); // REPLACE '3F' WITH SOMETHING. WILL ONLY BE USED WHEN THE PLAYER HAS A BASIC ATTACK FUNCTION THAT CAN KNOCK ENEMIES BACK.
            TakeDamage(3f, 10); //REPLACE WITH ENEMY DAMAGE NUMBERS

            //SOMETHING THAT CHECKS COROUTINE/BOOL DAMAFGE STUFF
            //playerAnim.SetTrigger("Take_Damage) ADD TAKE_DAMAGE ANIMATIONS
            if (currentHealth <= 0)
            {
                //GameOver();
            }
        }
        #endregion
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        #region Player & Enemy
        //ALSO SPAGHETTI. HIGHLY DEPENDENT. THIS ONLY CHECKS FOR ONCOLLISION AND NOT ONTRIGGER. THE CIRCLECOLLIDER OF ABSORB DOES NOT MATCH THIS, SO YOU HAVE TO BUMP INTO THE ENEMY IN ORDER FOR THIS TO WORK, AND NOT THE ABSORB ABILITY.
        if (other.gameObject.CompareTag("Enemy") && abilityHolder.CompareTag("Absorb"))
        {
            absorbAdd(chargeAmount);
        }
        #endregion

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

    //SIMPLE DAMAGE SCRIPT
    public void playerGetHit(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    //TAKE DAMAGE AND GET KNOCKED BACK
    public void TakeDamage(float knockTime, int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            //DISABLES PLAYER GAMEOBJECT ONCE HEALTH IS < 0
            gameObject.SetActive(false);
        }
    }

    //ADDS TO THE ABSORB METER WHEN PLAYER IS ABSORBING. NOT SURE HOW I FEEL ABOUT THIS DEPENDANCY.
    public void absorbAdd(float value)
    {
        currentCharge += value;
        absorbBar.SetValue(currentCharge);

        if (currentCharge >= 100)
        {
            Debug.Log(currentCharge);
            playerGetHit(100);
            StartCoroutine(FlashCo());
            currentCharge = 0;
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
        triggerCollider.enabled = false;

        while (temp < numberOfFlashes)
        {
            playerSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }

    #endregion
}
