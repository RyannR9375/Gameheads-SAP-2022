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
        print(this.transform.GetChild(1).name);
        CheckIfRoom();

        /*
        if(doISeeADoor[0].transform.tag == "Room")
        {
            Debug.Log(doISeeADoor[0].transform.name + "RoomSEEN");
            Destroy(this.gameObject);
        }
        */

        //if(doISeeADoor.transform.tag == )

    }
    bool CheckIfRoom()
    {
        RaycastHit2D doISeeADoor = Physics2D.Raycast(this.transform.GetChild(1).position, transform.right);
        Debug.DrawRay(this.transform.GetChild(1).position, transform.right * 10, Color.red, 2f);
        if (doISeeADoor)
        {
            return false;
        }
        else
        {
            return true;
        }
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
            if (CheckIfRoom())
            {
                GameObject room = LevelGenerator.SpawnRoom(WhatDirection, spawnLocation);
                Destroy(this.gameObject);
            }
        } 
    }
}
