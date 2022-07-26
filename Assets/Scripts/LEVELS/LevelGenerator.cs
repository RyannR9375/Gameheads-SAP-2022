using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] FloorTemplates = new GameObject[2];
    [SerializeField]
    public LevelSettings LevelSettings;
    [HideInInspector]
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(FloorTemplates[Random.Range(0, FloorTemplates.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static GameObject SpawnRoom(GameObject room,RoomSpawner.direction direction, Vector3 PrevRoomPos)
    {
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
        GameObject spawnedRoom = Instantiate(room, spawnLocation, new Quaternion(0,0,0,0), GameObject.Find("MAP").transform);
        var attributes = spawnedRoom.GetComponent<SpawnRoomItems>();
        attributes.DestroyUselessDoor(direction);
        return spawnedRoom;

    }
}
