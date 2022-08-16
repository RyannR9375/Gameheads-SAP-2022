using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private static CameraFollow instance;
    private GameObject CM;

    public PolygonCollider2D CameraBounds;

    SpawnRoomItems spawnRoomItems;
    private LevelGenerator LM;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    public static CameraFollow MyInstance
    {
        get
        {
            if (instance == null)
                instance = new CameraFollow();

            return instance;
        }
    }

    private void Start()
    {
        LM = GameObject.Find("GameManager").GetComponent<LevelGenerator>();
        //CameraBounds = LM.GetComponent<PolygonCollider2D>();
        //Spawn Room Items instead, array? Have rooms check if the player is in it, if they are, bool = true, And doko checks for this bool across the array
        //and then sets the polygon collider to the one in the room that the player is in.
        SpawnRoomItems[] doko = GameObject.FindObjectsOfType<SpawnRoomItems>();

        PolygonCollider2D LMCamera = LM.GetComponent<PolygonCollider2D>();
        LMCamera = CameraBounds;

        CM = GameObject.Find("CM vcam1").gameObject;
        Cinemachine.CinemachineConfiner2D confiner2D = CM.GetComponent<Cinemachine.CinemachineConfiner2D>();
        confiner2D.m_BoundingShape2D = CameraBounds;


    }
}
