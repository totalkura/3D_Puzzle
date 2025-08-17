using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    public Color color;
    public string GetPrompt()
    {
        return "ť���Դϴ� ����ѹ� F�� ����������";
    }

    public void Interact()
    {
        // Renderer ��������
        Renderer rend = GetComponent<Renderer>();

        if (rend != null)
        {
            // ���� �� ����
            Color randomColor = new Color(
                Random.value, // R
                Random.value, // G
                Random.value  // B
            );

            // ��Ƽ���� ���� ����
            rend.material.color = randomColor;
        }
    }

}
