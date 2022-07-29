using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public float speed;
    public bool isFlipped = false;

    public float followDistance;
    public float stoppingDistance;

    public GameObject boss;
    public GameObject prefabSimpleAttack;

    private Transform playerTransform;
    private GameObject playerGameObject;

    private Vector3 playerVector3;
    private Vector3 targetDirection;
    private Vector3 bossVector3;

    //public Transform[] moveSpots;
    private int randomSpot;

    // Start is called before the first frame update
    void Awake()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        playerVector3 = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
        bossVector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetDirection = playerTransform.position - transform.position;
        //randomSpot = Random.Range(0, moveSpots.Length);
    }

    public void Update()
    {
        //if (Vector2.Distance(transform.position, playerTransform.position) > stoppingDistance)
        //{
            //StartCoroutine(SimpleAttack());
        //}
    }

    private IEnumerator SimpleAttack()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(3f);
        //boss.anim
        GameObject attack = (GameObject)Instantiate(prefabSimpleAttack, transform);
        attack.SetActive(true);
        yield return new WaitForSeconds(0f);
        Destroy(attack);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > playerTransform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerTransform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    //public void FollowMe()
    //{
    //if (Vector2.Distance(transform.position, player.position) < talkingDistance)
    //{
    // transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
    // }
    //}
}
