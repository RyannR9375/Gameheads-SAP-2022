using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomItems : MonoBehaviour
{
    public GameObject[] EnemyTypes = new GameObject[1];
    public GameObject[] ItemsToSpawn = new GameObject[1];
    public GameObject spawnFolder;
    private Transform[] SpawnLocations = new Transform[0];
    private LevelGenerator LM;
    // Start is called before the first frame update
    void Start()
    {
        SpawnLocations = spawnFolder.gameObject.GetComponentsInChildren<Transform>();
        print(SpawnLocations[0]);
        LM = GameObject.Find("GameManager").GetComponent<LevelGenerator>();
        for (int i = 0; i < Random.Range(LM.LevelSettings.minEnemies, LM.LevelSettings.maxEnemies);i++) 
        {
            Instantiate(EnemyTypes[Random.Range(0, EnemyTypes.Length)], SpawnLocations[Random.Range(1,SpawnLocations.Length)].position,new Quaternion(0,0,0,0));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyUselessDoor(RoomSpawner.direction direction)
    {
        foreach (Transform child in this.gameObject.transform.GetChild(1).transform)
        {
            if (direction == RoomSpawner.direction.Up)
            {
                if (child.GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Down)
                {
                    Destroy(child.gameObject);
                    break;
                }
                print("Up");
            }
            if (direction == RoomSpawner.direction.Down)
            {
                if (child.GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Up)
                {
                    Destroy(child.gameObject);
                    break;
                }
                print("Down");
            }
            if (direction == RoomSpawner.direction.Left)
            {
                if (child.GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Right)
                {
                    Destroy(child.gameObject);
                    break;
                }
                print("Left");
            }
            if (direction == RoomSpawner.direction.Right)
            {
                if (child.GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Left)
                {
                    Destroy(child.gameObject);
                    break;
                }
                print("Right");
            }
        }
        
    }
}
