using UnityEngine;

public class CursorInterect : MonoBehaviour
{
    public Behaviour outline; // Outline 컴포넌트 연결

    void Start()
    {
        outline.enabled = false; // 처음에는 꺼두기
    }

    void OnMouseOver()
    {
        outline.enabled = true; // 마우스가 올라갔을 때 켜기
    }

    void OnMouseExit()
    {
        outline.enabled = false; // 마우스가 벗어날 때 끄기
    }
}
