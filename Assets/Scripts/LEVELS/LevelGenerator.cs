using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject[] FloorTemplates = new GameObject[5];

    [SerializeField]
    public LevelSettings LevelSettings;

    public GameObject gameManager;
    //private GameManager gameManagerScript;
    public bool TutorialLevel;
    public GameObject TutorialRoom;
    private bool tutorialRoomSpawned = false;
    public float roomCount = 0f;
    /*private int spawnedItem;
    public List<int> ResilienceChance = new List<int>();
    public int resilience1Count = 1;
    public int resilience2Count = 2;
    public int emptyCount = 0;
    */

    #region Unity Callback Functions
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").gameObject;
        //gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        /*for(int i = 0; i < resilience1Count; i++)
        {
            ResilienceChance.Add(1);
        }
        for (int i = 0; i < resilience2Count; i++)
        {
            ResilienceChance.Add(2);
        }
        for (int i = 0; i < emptyCount; i++)
        {
            ResilienceChance.Add(0);
        }*/

    }
    void Start()
    {
        if (!TutorialLevel)
        {
            SpawnTutorialRoom();

            if (tutorialRoomSpawned == false)
            {
                Instantiate(FloorTemplates[Random.Range(0, FloorTemplates.Length - 1)], GameObject.Find("MAP").transform);
            }
        }
        
        
    }

    void Update()
    {
        if (!gameManager.activeSelf)
        {
            gameManager.SetActive(true);
        }
        
        //Debug.Log(roomCount);
    }
    #endregion

    public List<GameObject> SpawnCorrectRoomDamnIt(RoomSpawner.direction direction)
    {
        List<GameObject> newList = new List<GameObject>();
        foreach(GameObject room in FloorTemplates)
        {
            for (int i = 0; i < room.transform.GetChild(1).transform.childCount; i++)
            {
                
                if (direction == RoomSpawner.direction.Down && room.transform.GetChild(1).transform.GetChild(i).GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Up)
                {
                    newList.Add(room);
                }
                if (direction == RoomSpawner.direction.Up && room.transform.GetChild(1).transform.GetChild(i).GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Down)
                {
                    newList.Add(room);
                }
                if (direction == RoomSpawner.direction.Right && room.transform.GetChild(1).transform.GetChild(i).GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Left)
                {
                    newList.Add(room);
                }
                if (direction == RoomSpawner.direction.Left && room.transform.GetChild(1).transform.GetChild(i).GetComponent<RoomSpawner>().WhatDirection == RoomSpawner.direction.Right)
                {
                    newList.Add(room);
                }
            }
        }

        //spawnedItem = Chance();
        roomCount += 1;

        return newList;
    }
    public static GameObject SpawnRoom(RoomSpawner.direction direction, Vector3 PrevRoomPos)
    {
        var LM = new LevelGenerator();
        LM = GameObject.Find("GameManager").GetComponent<LevelGenerator>();
        List<GameObject> newFloorTemplates = LM.SpawnCorrectRoomDamnIt(direction);
        Vector3 spawnLocation = PrevRoomPos;
        if (direction == RoomSpawner.direction.Up)
        {
            spawnLocation = new Vector3(PrevRoomPos.x, PrevRoomPos.y + 23.18f);
        }
        if (direction == RoomSpawner.direction.Down)
        {
            spawnLocation = new Vector3(PrevRoomPos.x, PrevRoomPos.y + -23.18f);
        }
        if (direction == RoomSpawner.direction.Left)
        {
            spawnLocation = new Vector3(PrevRoomPos.x + -17.6f, PrevRoomPos.y);
        }
        if (direction == RoomSpawner.direction.Right)
        {
            spawnLocation = new Vector3(PrevRoomPos.x + 17.6f, PrevRoomPos.y);
        }
        
        if (GameManager.MyInstance.CollectedItems < GameManager.MyInstance.victoryCondition)
        {
            GameObject spawnedRoom = Instantiate(newFloorTemplates[Random.Range(0, newFloorTemplates.Count - 1)], spawnLocation, new Quaternion(0, 0, 0, 0), GameObject.Find("MAP").transform);
            SpawnRoomItems roomScript = spawnedRoom.GetComponent<SpawnRoomItems>();
            roomScript.DestroyUselessDoor(direction);

            //RoomLock roomLock = spawnedRoom.GetComponent<RoomLock>();
            //Debug.Log(roomLock.enemiesAlive);
            //BoxCollider2D doorCollider = spawnedRoom.GetComponent<RoomSpawner>().gameObject.GetComponent<BoxCollider2D>();
            //doorCollider.isTrigger = false;
            
            return spawnedRoom;
        }
        else
        {
            GameObject spawnedRoom = Instantiate(newFloorTemplates[Random.Range(0, newFloorTemplates.Count)], spawnLocation, new Quaternion(0, 0, 0, 0), GameObject.Find("MAP").transform);
            SpawnRoomItems roomScript = spawnedRoom.GetComponent<SpawnRoomItems>();
            roomScript.DestroyUselessDoor(direction);
            return spawnedRoom;
        }

    }

    public void SpawnTutorialRoom()
    {
        RoomSpawner tutRoomSpawnerScript = TutorialRoom.GetComponent<RoomSpawner>();

        Instantiate(TutorialRoom, GameObject.Find("MAP").transform);
        tutorialRoomSpawned = true;
    }

    /*public int Chance()
    {

        if (ResilienceChance.Count == 0)
        {
            return 0;
        }

        int chanceIndex = Random.Range(0, ResilienceChance.Count);

        int chanceValue = ResilienceChance[chanceIndex];

        ResilienceChance.RemoveAt(chanceIndex);

  
        return chanceValue;
    }*/
}
