using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileType : MonoBehaviour
{
    //CALLING SCRIPTS
    private Enemy enemy;
    public Projectile projectile;
    private PlayerMovement player;

    //DECLARING VARIABLES
    float startTimeBtwShots;
    float activeTime;
    float cooldownTime;

    //CREATING TYPES OF SHOOT STATES
    enum ShootState
    {
        ready,
        active,
        cooldown
    }

    //AUTOMATICALLY SETTING SHOOT STATE TO TRUE
    ShootState state = ShootState.ready;

    

    void Update() // IF STATEMENT IF THIS.COMPARETAG('PLAYER') <SWITCH STATE ACTIVATE, ETC BASED ON KEY INPUT>, IF ENEMY, STICK WITH CIRCLE COLLIDER
    {
        switch (state)
        {
            //READY STATE
            case ShootState.ready:
                if (Input.GetKeyDown(KeyCode.E))//PLAYER IS IN VICINITY
                {
                    Debug.Log("Not - ready");
                    projectile.Activate(gameObject, gameObject);
                    state = ShootState.active;
                }
                break;

            //ACTIVE STATE
            case ShootState.active:
                if (activeTime > 0)//BULLETS ACTIVE >= 4. MIGHT BE THE VALUE YOU WANT TO PLAY AROUND WITH,
                {
                    Debug.Log("Not - active");
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("Not - activeElse");
                    projectile.BeginCooldown(gameObject);
                    state = ShootState.cooldown;
                    activeTime = projectile.cooldownTime;
                }
                break;

            //COOLDOWN STATE
            case ShootState.cooldown:
                if (activeTime > 0)//BULLETS ACTIVE < 4 MIGHT BE THE VALUE YOU WANT TO PLAY AROUND WITH,
                {
                    Debug.Log("Not - cooldown");
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("Not - cooldownElse");
                    state = ShootState.ready;
                }
                break;
        }
    }
}
