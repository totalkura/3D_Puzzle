using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class OpenDoorCube : MonoBehaviour, IInteractable
{
    Door door;
    public string GetPrompt()
    {
        return "잘하면 문을 열 수 있을지도?";
    }

    public void Interact()
    {
        door = GetComponentInParent<Door>();
        door.DoorOpen();
        //문에대한정보
    }

}
