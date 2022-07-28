using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public direction WhatDirection = new direction();
    private Transform Room;
    public enum direction
    {
        Up,
        Down,
        Left,
        Right
    };
    
    // Start is called before the first frame update
    void Start()
    {
        Room = this.transform.parent.transform.parent;
        print(Room.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            Vector3 spawnLocation = Room.transform.position;
            LevelGenerator.SpawnRoom(WhatDirection, spawnLocation);
            /*RaycastHit2D doISeeADoor = Physics2D.Linecast(this.gameObject.transform.position, this.gameObject.transform.position);

            if (WhatDirection == direction.Up)
            {
                doISeeADoor = Physics2D.Linecast(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.5f), new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 50.5f));
            }
            if (WhatDirection == direction.Down)
            {
                doISeeADoor = Physics2D.Linecast(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + -1.5f), new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + -2.5f));
            }
            if (WhatDirection == direction.Left)
            {
                doISeeADoor = Physics2D.Linecast(new Vector2(this.gameObject.transform.position.x + -1.5f, this.gameObject.transform.position.y), new Vector2(this.gameObject.transform.position.x+-2.5f, this.gameObject.transform.position.y));
            }
            if (WhatDirection == direction.Right)
            {
                doISeeADoor = Physics2D.Linecast(new Vector2(this.gameObject.transform.position.x + 1.5f, this.gameObject.transform.position.y), new Vector2(this.gameObject.transform.position.x +2.5f, this.gameObject.transform.position.y));
            }
            if (doISeeADoor.transform.gameObject.tag == "Door")
            {
                Destroy(doISeeADoor.transform.gameObject);
            }
            */
            Destroy(this.gameObject);
        }
        
    }
}
