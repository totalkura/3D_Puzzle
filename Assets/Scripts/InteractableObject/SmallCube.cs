using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCube : MonoBehaviour, IInteractable
{
    public string GetPrompt()
    {
        return "�ǵ�� �۾����� ���ƿ�";
    }

    public void Interact()
    {
        CharacterManager.Instance.Player.transform.localScale = Vector3.one * 0.1f;
    }

   
}
