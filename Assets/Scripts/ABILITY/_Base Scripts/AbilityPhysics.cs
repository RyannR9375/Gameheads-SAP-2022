using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPhysics : MonoBehaviour
{
    /// <summary>
    /// POTENTIALLY WORK ON MAKING AN ARRAY THAT ACCEPTS THE SAME VALUES AS 'AbilityList[]' THAT WAY WE DONT HAVE TO ADD 'AbilityPhysics' SCRIPT INTO THE INSPECTOR.
    /// </summary>

    //CALLING SCRIPTS
    public ShineAbility shineAbility;
    public AbsorbAbility absorbAbility;
    public ReleaseAbility releaseAbility;
    //public AbsorbBar absorbBar;

    //OnTriggerEnter2D Collision FOR ABILITIES. ABILITIES AND ENEMIES DIFFERENTIATED THROUGH ABILITIES TAGS; 'Shine', 'Ultimate', etc...
    private void OnTriggerEnter2D(Collider2D other)
    {
        //SHINE ABILITY TRIGGER COLLISION. CHECKS FOR BOTH 'ENEMY' TAG AND 'SHINE' TAG.
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Shine"))
        {
            //GETTING THE ENEMY'S RIGIDBODY 2D IN ORDER TO EFFECT IT
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            //CHECKING IF ENEMY HAS Rigidbody2D 
            if (enemy != null)
            {
                //PUSH FORCE MATH. '.FORCE' FELT BETTER THAN '.IMPULSE'
                Vector2 difference = (enemy.transform.position - transform.position);
                difference = (difference.normalized * shineAbility.shinePush);
                enemy.AddForce(difference, ForceMode2D.Force);
                StartCoroutine(KnockTime(enemy));
                Debug.Log("Calling Shine AbilityPhysics");
            }
        }

        //ABSORB ABILITY TRIGGER COLLISION. CHECKS FOR BOTH 'ENEMY' TAG AND 'ABSORB' TAG.
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Absorb"))
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();

            if (enemy != null)
            {
                //PLAY ENEMY DEATH ANIM, AND THEN POTENTIALLY DEACTIVATE EVERYTHING EXCEPT THE SPRITE RENDERER/ANIMATOR?
                //Destroy(enemy);
                Debug.Log("Calling Absorb AbilityPhysics");
            }
        }

        //RELEASE ABILITY TRIGGER COLLISION.CHECKS FOR BOTH 'ENEMY' TAG AND 'RELEASE' TAG.
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Release"))
        {
            //GETTING THE ENEMY'S RIGIDBODY 2D IN ORDER TO EFFECT IT
            GameObject enemy = other.gameObject;
            //CHECKING IF ENEMY HAS Rigidbody2D 
            if (enemy != null)
            {
                //PUSH FORCE MATH. '.FORCE' FELT BETTER THAN '.IMPULSE'
                //Vector2 difference = (enemy.transform.position - transform.position);
                //difference = (difference.normalized * releaseAbility.releasePush);
                //enemy.AddForce(difference, ForceMode2D.Force);
                //StartCoroutine(KnockTime(enemy));
                Debug.Log("Calling Release AbilityPhysics");
                Destroy(enemy);
            }
        }
    }

    //SETS UP THE AMOUNT OF TIME WE WANT THE FORCE TO BE APPLIED BEFORE RESETTING THE ENEMY'S VELOCITY TO 0 SO IT STOPS MOVING.
    private IEnumerator KnockTime(Rigidbody2D enemy)
    {
        if (enemy != null && enemy.velocity != Vector2.zero)
        {
            yield return new WaitForSeconds(shineAbility.knockbackTime);
            enemy.velocity = Vector2.zero;
        }
    }

}
