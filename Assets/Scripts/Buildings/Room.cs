using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoomState
{
    None = 0,
    Front = 1,
    Back = 2,
    Left = 4,
    Right = 8,
}

[SelectionBase]
public class Room : MonoBehaviour
{
    public GameObject[] walls = new GameObject[4];
    public GameObject[] columns = new GameObject[4];
    public RoomAttach[] attachs = new RoomAttach[4];

    public Room[] rooms = new Room[4];


    public void UpdateRoom (int n)
    {
        foreach (GameObject wall in walls)
        {
            if (wall == null) continue;
            wall.SetActive(true);
        }

        while (n != 0)
        {
            int i = Mathf.IsPowerOfTwo(n) ? Mathf.ClosestPowerOfTwo(n) : Mathf.NextPowerOfTwo(n)/2;
            int v = (int)Mathf.Log(i, 2);
            n -= i;
            walls[v].SetActive(false);
        }
    }

    private void OnValidate()
    {
        int n = 0;
        for (int i = 0; i < rooms.Length; i++)
        {
            var room = rooms[i];
            var attach = attachs[i];
            if (!room && !attach) continue;
            n += 1 << i;
        }

        UpdateRoom(n);
    }
}
