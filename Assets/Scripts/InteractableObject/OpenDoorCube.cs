using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorCube : MonoBehaviour, IInteractable
{
    public string GetPrompt()
    {
        return "잘하면 문을 열 수 있을지도?";
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

}
