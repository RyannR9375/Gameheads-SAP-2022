using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] AllowedRooms = new GameObject[2];
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

            GameObject newRoom = LevelGenerator.SpawnRoom(AllowedRooms[Random.Range(0, AllowedRooms.Length)], WhatDirection, spawnLocation);
            Destroy(this.gameObject);
        }
        
    }
}
