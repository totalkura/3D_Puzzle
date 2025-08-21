using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    public Color color;
    public int switchCheck;
    public string GetPrompt()
    {
        return "큐브입니다 어디한번 F를 눌러보세요";
    }

    public void Interact()
    {
        PortalManager.Instance.CheckSwitch(switchCheck);
        // Renderer 가져오기
        Renderer rend = GetComponent<Renderer>();

        if (rend != null)
        {
            // 랜덤 색 생성
            Color randomColor = new Color(
                Random.value, // R
                Random.value, // G
                Random.value  // B
            );

            // 머티리얼 색상 변경
            rend.material.color = randomColor;
        }
    }

}
