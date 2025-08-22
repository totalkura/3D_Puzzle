using System.Collections.Generic;
using UnityEngine;

public class CheckLaser : MonoBehaviour
{
    [Header("Object Check")]
    [SerializeField] private List<GameObject> laserObjects;
    public List<bool> checks_One ;

    public Door door;

    int boolCheckCount;

    private void Start()
    {
        laserObjects = new List<GameObject>();

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name.StartsWith("Laser"))
            {
                laserObjects.Add(child.gameObject);
            }
            else if (child.name == "Door")
            {
                door = child.GetComponent<Door>();
            }
        }

        checks_One = new List<bool>(new bool[laserObjects.Count]);
    }

    public void OnCubeCheck(int checkNum)
    {
        checks_One[checkNum] = true;
        boolCheckCount++;
        laserObjects[checkNum].SetActive(false);
        if (CheckBoolCount()) door.DoorOpen();

    }

    public void OutCubeCheck(int checkNum)
    {
        checks_One[checkNum] = false;
        boolCheckCount--;
        laserObjects[checkNum].SetActive(true);
        if (!CheckBoolCount()) door.DoorClose();
    }
    public bool CheckBoolCount()
    {
        if(checks_One.Count == boolCheckCount)
            return true;
        return false;
    }
}
