using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;

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


}
