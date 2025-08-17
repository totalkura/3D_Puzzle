using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCube : MonoBehaviour, IInteractable
{
    public string GetPrompt()
    {
        return "F를누르면 들 수 있습니다.";
    }

    public void Interact()
    {
        
    }
}
