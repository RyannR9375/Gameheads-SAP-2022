using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject[] FloorTemplates = new GameObject[4];
    [SerializeField]
    public LevelSettings LevelSettings;
    [HideInInspector]
    void Start()
    {
        Instantiate(FloorTemplates[Random.Range(0, FloorTemplates.Length)], GameObject.Find("MAP").transform);
    }

    void Update()
    {
        
    }
    public static GameObject SpawnRoom(RoomSpawner.direction direction, Vector3 PrevRoomPos)
    {
        var LM = new LevelGenerator();
        LM = GameObject.Find("GameManager").GetComponent<LevelGenerator>();
        Vector3 spawnLocation = PrevRoomPos;
        if (direction == RoomSpawner.direction.Up)
        {
            spawnLocation = new Vector3(PrevRoomPos.x, PrevRoomPos.y + 24f);
        }
        if (direction == RoomSpawner.direction.Down)
        {
            spawnLocation = new Vector3(PrevRoomPos.x, PrevRoomPos.y + -24f);
        }
        if (direction == RoomSpawner.direction.Left)
        {
            spawnLocation = new Vector3(PrevRoomPos.x + -18, PrevRoomPos.y);
        }
        if (direction == RoomSpawner.direction.Right)
        {
            spawnLocation = new Vector3(PrevRoomPos.x + 18f, PrevRoomPos.y);
        }
        GameObject spawnedRoom = Instantiate(LM.FloorTemplates[Random.Range(0, LM.FloorTemplates.Length)], spawnLocation, new Quaternion(0,0,0,0), GameObject.Find("MAP").transform);
        var attributes = spawnedRoom.GetComponent<SpawnRoomItems>();
        attributes.DestroyUselessDoor(direction);
        return spawnedRoom;

    }
}
