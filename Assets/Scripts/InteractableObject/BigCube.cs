using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCube : MonoBehaviour, IInteractable
{
    public string GetPrompt()
    {
        return "�ǵ�� Ŀ���� ���ƿ�!";
    }

    public void Interact()
    {
        CharacterManager.Instance.Player.transform.localScale = new Vector3(1f, 1.8f, 1f);
    }
}
