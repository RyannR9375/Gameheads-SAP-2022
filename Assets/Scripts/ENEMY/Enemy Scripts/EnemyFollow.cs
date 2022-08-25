using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour 
{ 

    public float speed;
    public GameObject playerTransform;
    public float followDistance;
    public float damage;
    private float take;

    public Transform[] moveSpots;
    private int randomSpot;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").gameObject;
        if (moveSpots[0] == null)
        {
            moveSpots[0] = playerTransform.transform;
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player");
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, transform.position) < followDistance) //fix FIX FIX FIX FIX
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        }
    }

    //public void FollowMe()
    //{
    //if (Vector2.Distance(transform.position, player.position) < talkingDistance)
    //{
    // transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
    // }
    //}

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player playerScript = playerTransform.GetComponent<Player>();
            playerScript.playerGetHit(damage);
        }

    }

}
