using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public direction WhatDirection = new direction();
    private Transform Room;
    private bool tutorialRoomPassed = false;
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
        RaycastHit2D doISeeADoor = Physics2D.Raycast(this.transform.GetChild(1).position, transform.right,5f);
        Debug.DrawRay(this.transform.GetChild(1).position, transform.right * 10, Color.red, 2f);
        if (doISeeADoor)
        {
            if (doISeeADoor.transform.tag == "Door")
            {
                Destroy(doISeeADoor.transform.gameObject);
                Destroy(this.gameObject);
            }
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
            else
            {
                print("No Room!");
            }
        }

        if(this.CompareTag("TutorialRoomDoor") && collision.CompareTag("Player"))
        {
            tutorialRoomPassed = true;
        }
    }
}
