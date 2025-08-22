using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordHint : MonoBehaviour , IInteractable
{
    public bool isClear;
    public Player player; // Player reference

    
    public string clearedPrompt = "��й�ȣ�� �������ڰ� �տ� ���� ���������̴�.";
    public string notClearedPrompt = "�̰��� ������?";

    public void Interact()
    {
        isClear = !isClear;// �ʱ�ȭ
        gameObject.SetActive(true);
        Debug.Log("��й�ȣ ��Ʈ ��ȣ�ۿ�");                
    }

    public string GetPrompt()
    {
        if (isClear)
        {
            return clearedPrompt;
        }
        else
        {
            return notClearedPrompt;
        }
    }
}
