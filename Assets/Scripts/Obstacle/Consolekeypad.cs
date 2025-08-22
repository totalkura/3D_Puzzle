using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class ConsoleKeypad : MonoBehaviour, IInteractable
{
    GameObject door;
    public bool isClear;
    public GameObject canvus; 
    public TMP_Text displayText;   // 입력 숫자 표시
    [SerializeField]
    private string currentInput = "";

    public string correctCode = "1234"; // 정답 코드 (퍼즐 정답)
      
    public Player player; // 플레이어 참조
          
    void Start()
    {
        door=GetComponent<Door>().gameObject; // Door 컴포넌트에서 문 오브젝트 가져오기
    }
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
            ExitButton();
            isClear = true;
            door.GetComponent<Door>().DoorOpen(); // 문 열기
            // 여기서 문 열기 애니메이션 등 실행
            if (player != null)
            {
                // 예시: 플레이어 상태 변경
                player.controller.characterGetItem = true;
            }
        }
        else
        {
            Debug.Log("틀렸습니다");
            currentInput = "";
            displayText.text = currentInput;
            CharacterManager.Instance.Player.condition.HasHealth(20);
        }
    }
    public void viewCanvus()
    {
        currentInput = "";
        displayText.text = currentInput;
        canvus.SetActive(true); // 캔버스 활성화
    }

    public void Interact()
    {
        CharacterManager.Instance.Player.controller.ToggleCursor();
        currentInput = ""; // 입력 초기화
        viewCanvus();
        Time.timeScale = 0; // 게임 일시 정지       
        Debug.Log("콘솔 키패드와 상호작용합니다.");
    }
      
    public string GetPrompt()
    {
        if (isClear)
        {
            return "퍼즐을 완료했습니다. 문이 열렸습니다.";
        }
        return "콘솔 키패드와 상호작용하려면 버튼을 누르세요.";
        
    }
    public void ExitButton()
    {
        CharacterManager.Instance.Player.controller.ToggleCursor();

        Time.timeScale = 1; // 게임 재개
        canvus.SetActive(false); // 캔버스 비활성화
    }
}

