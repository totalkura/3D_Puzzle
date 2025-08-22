using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordHint : MonoBehaviour , IInteractable
{
    public bool isClear;
    public Player player; // Player reference

    
    public string clearedPrompt = "비밀번호는 작은숫자가 앞에 오고 등차수열이다.";
    public string notClearedPrompt = "이곳에 편지가?";

    public void Interact()
    {
        isClear = !isClear;// 초기화
        gameObject.SetActive(true);
        Debug.Log("비밀번호 힌트 상호작용");                
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
