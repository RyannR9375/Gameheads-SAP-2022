using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// CALLING SCRIPTS, AND DECLARING VARIABLES
    /// </summary>

    //DECLARING COMPONENTS
    Vector2 movement;
    Rigidbody2D rb;
    Collider2D triggerCollider;
    SpriteRenderer playerSprite;
    public HealthBar healthBar;
    public AbsorbBar absorbBar;

    //SPAGHETTI
    public GameObject abilityHolder;
    //public AbilityHolderDefense abilityHolderScript;

    //VARIABLES FOR FUNCTIONS
    private Vector3 scaleChange;
    private Vector3 minScale;
    private float minScaleMag;

    //PLAYER
    [Header("Player Stats")]
    public float moveSpeed;
    public int maxHealth;
    public int currentHealth;

    [Header("IFrame")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;

    [Header("Ability Meter")]
    //ABILITY BAR
    public float maxCharge;
    public float currentCharge;
    public float chargeAmount;

    /// <summary>
    /// AWAKE, UPDATE, FIXED UPDATE, ONTRIGGERENTER2D
    /// </summary>
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        triggerCollider = GetComponent<Collider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        
        //refactor to scriptable objects
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;

        //spaghetti
        scaleChange = new Vector3(-1f, -1f, -0f);
    }
    void Update()
    {
        PlayerInput();
    }
    void FixedUpdate()
    {
        ApplyMovement();

        //SPAGHETTI
        tag = "Player";

    }

    private void OnCollisionEnter2D(Collision2D other) // POTENTIALLY MOVING THIS TO IF THE ENEMY COLLIDES WITH THE PLAYER INSTEAD OF PLAYER COLLIDING WITH ENEMY, JUST TO KEEP PLAYER SCRIPTS CLEAN.
    {
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player"))
        {
            //StartCoroutine(KnockCo(3f)); // REPLACE '3F' WITH SOMETHING. WILL ONLY BE USED WHEN THE PLAYER HAS A BASIC ATTACK FUNCTION THAT CAN KNOCK ENEMIES BACK.
            TakeDamage(3f, 10); //REPLACE WITH ENEMY DAMAGE NUMBERS

            //playerAnim.SetTrigger("Take_Damage) ADD TAKE_DAMAGE ANIMATIONS
            if (currentHealth <= 0)
            {
                GameOver();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //ALSO SPAGHETTI. HIGHLY DEPENDENT. THIS ONLY CHECKS FOR ONCOLLISION AND NOT ONTRIGGER. THE CIRCLECOLLIDER OF ABSORB DOES NOT MATCH THIS, SO YOU HAVE TO BUMP INTO THE ENEMY IN ORDER FOR THIS TO WORK, AND NOT THE ABSORB ABILITY.
        if (other.gameObject.CompareTag("Enemy") && abilityHolder.CompareTag("Absorb"))
        {
            absorbRelease(other, chargeAmount);
        }
    }

    /// <summary>
    /// PLAYER FUNCTIONS
    /// </summary>

    //KEYBINDS FOR MOVEMENT
    private void PlayerInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    public void ApplyMovement()
    {
        Vector3 moveDir = new Vector2(movement.x, movement.y).normalized;
        transform.position += moveDir * moveSpeed * Time.fixedDeltaTime;
    }
    
    //SIMPLE DAMAGE FUNCTION MAINLY USED FOR DEBUGGING.
    void playerGetHit(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    //RESTARTS CURRENT SCENE, WILL EVENTUALLY BRING UP UI THROUGH AN 'ISPAUSED' BOOL WITH OPTIONS SUCH AS RETURN, EXIT, OR TRY AGAIN.
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //PLAYER HEALTH FACTORS -- POTENTIALLY MOVE THIS INTO JUST THE ENEMY SCRIPTS RATHER THAN MAKING THE PLAYER DEPENDENT ON THIS?
    //GAIN STRENGTH FROM ABSORBING ENEMY HEALTH? SOME SORT OF WEIRD METAPHOR FOR DRAINING OR USING ENERGY TO YOUR ADVANTAGE? RELEASE DOES SOME SORT OF PLAYER DAMAGE? REPRESENTATIVE OF 'TOXIC POSITIVITY'?
    public void TakeDamage(float knockTime, int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //SPAGHETTI FOR NOW. CHANGEEEEEEEEEEEEEEEEEEEEE THIS. MOVE THIS.
    void absorbRelease(Collider2D enemy, float value)
    {
        tag = "Absorb";
        currentCharge += value;
        absorbBar.SetValue(currentCharge);
        //HARDCODING. BAD.
        scaleChange = new Vector3(-0.003f, -0.003f, 0f);
        minScale = new Vector3(0.2f, 1.077f, 0f);
        minScaleMag = (minScale.magnitude);

        if (currentCharge >= 100)
        {
            Debug.Log(currentCharge);
            playerGetHit(100);
            StartCoroutine(FlashCo());
            currentCharge = 0;
            absorbBar.SetValue(currentCharge);
        }

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

    //STOPS CHARACTER MOVEMENT FOR A SECOND AFTER YOU GET HIT IN ORDER TO ACTIVATE IFRAMES AND APPLY KNOCKBACK
    private IEnumerator KnockCo(float knockTime)
    {
        if(rb != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            //PLAYER ANIM
            rb.velocity = Vector2.zero;
        }
    }

    //CHARACTER FLASHING BECAUSE OF DAMAGE
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < numberOfFlashes)
        {
            playerSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
    //DASH . SPAGHETTI MOVE THIS LATER.
    
}
