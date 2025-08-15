using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class OpenDoorCube : MonoBehaviour, IInteractable
{
    Door door;
    public string GetPrompt()
    {
        return "���ϸ� ���� �� �� ��������?";
    }

    public void Interact()
    {
        door = GetComponentInParent<Door>();
        door.DoorOpen();
        //������������
    }

}
