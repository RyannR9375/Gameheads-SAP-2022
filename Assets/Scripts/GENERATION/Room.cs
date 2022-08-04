using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int width;

    public int height;

    public int X;

    public int Y;

    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You Pressed Play in the wrong scene");
            return;
        }
    }

    //DEBUGGING
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * width, Y * height);
    }
}
