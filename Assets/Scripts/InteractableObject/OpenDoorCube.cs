using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorCube : MonoBehaviour, IInteractable
{
    public string GetPrompt()
    {
        return "���ϸ� ���� �� �� ��������?";
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

}
