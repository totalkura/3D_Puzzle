using UnityEngine;
using TMPro;

public class ConsoleKeypad : MonoBehaviour
{
    public TMP_Text displayText;   // �Է� ���� ǥ��
    private string currentInput = "";

    public string correctCode = "1234"; // ���� �ڵ� (���� ����)

    // ��ư���� ȣ��� �Լ�
    public void OnNumberButton(string number)
    {
        if (currentInput.Length < 4) // �Է� ���� (��: 4�ڸ�)
        {
            currentInput += number;
            displayText.text = currentInput;
        }
    }

    public void OnClearButton()
    {
        currentInput = "";
        displayText.text = currentInput;
    }

    public void OnEnterButton()
    {
        if (currentInput == correctCode)
        {
            Debug.Log("����! ���� �����ϴ�");
            // ���⼭ �� ���� �ִϸ��̼� �� ����
        }
        else
        {
            Debug.Log("Ʋ�Ƚ��ϴ�");
            currentInput = "";
            displayText.text = currentInput;
        }
    }
}

