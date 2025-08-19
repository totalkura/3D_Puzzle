using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonOutline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Outline outline;   // 버튼(또는 아이콘)에 있는 Outline

    void Awake()
    {
        if (outline == null) outline = GetComponent<Outline>();
        if (outline != null) outline.enabled = false; // 기본은 꺼두기
    }

    // 마우스가 버튼에 닿았을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (outline != null) outline.enabled = true;
    }

    // 마우스가 버튼에서 벗어났을 때
    public void OnPointerExit(PointerEventData eventData)
    {
        if (outline != null) outline.enabled = false;
    }
}

