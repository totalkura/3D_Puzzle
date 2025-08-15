using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    public Door door;
    public int doorNumCheck;

    public static MapManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MapManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        
    }

    public void GetDoor(GameObject doorObject)
    {
        door = doorObject.GetComponentInChildren<Door>();
        doorNumCheck = door.doorNum;
    }

}
