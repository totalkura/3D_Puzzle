public interface IInteractable
{
    void Interact(); //상호작용시 발생할 행동 예를들어 SetBool(True)
    string GetPrompt(); // UI에 표시할 텍스트 (예: "F - 문 열기") 예를 들어 return "문 열기" 또는 if문사용 "문이 잠겨있다."/"문이 열려있다."
}