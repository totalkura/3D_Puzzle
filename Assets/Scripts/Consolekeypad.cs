using UnityEngine;
using TMPro;

public class ConsoleKeypad : MonoBehaviour
{
    public TMP_Text displayText;   // 입력 숫자 표시
    private string currentInput = "";

    public string correctCode = "1234"; // 정답 코드 (퍼즐 정답)

    // 버튼에서 호출될 함수
    public void OnNumberButton(string number)
    {
        if (currentInput.Length < 4) // 입력 제한 (예: 4자리)
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
            Debug.Log("정답! 문이 열립니다");
            // 여기서 문 열기 애니메이션 등 실행
        }
        else
        {
            Debug.Log("틀렸습니다");
            currentInput = "";
            displayText.text = currentInput;
        }
    }
}

