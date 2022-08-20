using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CinemachineVirtualCamera CineCamera;
    // Start is called before the first frame update
    void Start()
    {
        CineCamera = this.transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateCamera(SpawnRoomItems room)
    {
        //CineCamera.LookAt = room.transform;
        CineCamera.Follow = room.transform.GetChild(4).GetComponent<Transform>();
    }
    public static void TutorailUpdateCamera(SpawnRoomItems room)
    {
        CineCamera.Follow = room.transform.GetChild(4).GetComponent<Transform>();
    }
}
