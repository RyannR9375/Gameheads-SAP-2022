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
    private RaycastHit2D[] searchArea;

    private GameObject cameraPoint;
    private bool isHere = false;
    void Start()
    {
        SpawnLocations = spawnFolder.gameObject.GetComponentsInChildren<Transform>();
        //print(SpawnLocations[0]);
        LM = GameObject.Find("GameManager").GetComponent<LevelGenerator>();
        for (int i = 0; i < Random.Range(LM.LevelSettings.minEnemies, LM.LevelSettings.maxEnemies); i++)
        {
            Instantiate(EnemyTypes[Random.Range(0, EnemyTypes.Length)], SpawnLocations[Random.Range(1, SpawnLocations.Length)].position, new Quaternion(0, 0, 0, 0), GameObject.Find("ENEMIES").transform);
        }
        for (int i = 0; i < Random.Range(LM.LevelSettings.minEnemies, LM.LevelSettings.maxEnemies); i++)
        {
            Instantiate(ItemsToSpawn[Random.Range(0, ItemsToSpawn.Length)], SpawnLocations[Random.Range(1, SpawnLocations.Length)].position, new Quaternion(0, 0, 0, 0), GameObject.Find("RESILIENCE").transform);
        }

    }
    void Update()
    {

    }

    public void DestroyUselessDoor(RoomSpawner.direction direction)
    {
        searchArea = Physics2D.CircleCastAll(this.transform.position, 49f, transform.forward, float.PositiveInfinity, 6);
        foreach (Transform child in this.gameObject.transform.GetChild(1).transform)
        {
            if (direction == RoomSpawner.direction.Up)
            {
                if (child.GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Down)
                {
                    Destroy(child.gameObject);
                    //print("Up");
                    break;
                }
                continue;

            }
            if (direction == RoomSpawner.direction.Down)
            {
                if (child.GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Up)
                {
                    Destroy(child.gameObject);
                    //print("Down");
                    break;
                }
                continue;

            }
            if (direction == RoomSpawner.direction.Left)
            {
                if (child.GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Right)
                {
                    Destroy(child.gameObject);
                    //print("Left");
                    break;
                }
                continue;

            }
            if (direction == RoomSpawner.direction.Right)
            {
                if (child.GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Left)
                {
                    Destroy(child.gameObject);
                    //print("Right");
                    break;
                }
                continue;
            }

        }
    }
   
        //TODO: functionality for detecting if rooms are around the spawned room so we can delete the correct doors//

        /*foreach (RaycastHit2D unfilterdColliders in searchArea)
        {
            if (searchArea.Contains())
            {
                print(unfilterdColliders.transform.name);
                searchArea.
                
            }
        }
        foreach (RaycastHit2D room in searchArea)
        {
            if (room.distance > 17 && room.distance < 24)
            {
                print(room.transform.position);
            }
        }
    public void IsTheirARoom()
    {

    }
        */
}
