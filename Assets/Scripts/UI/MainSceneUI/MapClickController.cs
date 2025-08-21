using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapClickController : MonoBehaviour
{
    [SerializeField]
    private ScrollRect myScrollRect; // 스크롤바 컴포넌트

    public float distanceFactor = 0.1f; // 거리 조정 계수

    public void Start()
    {
        MapValueChanged(Vector2.zero);
    }

    public void MapValueChanged(Vector2 value)
    {
        for (int i = 0; i < myScrollRect.content.childCount; i++) //childCount = 9
        {
          

            //==============Stage의 위치값을 가져와서 거리 계산하기=================
            Vector2 posiotion = myScrollRect.content.GetChild(i).position; // Stages의 값은 W Data : -1550, H Data : 110
            Vector2 viewCenter = myScrollRect.viewport.position; // viewport의 값은 바텀 : 17

            // ==============거리 계산 및 스케일 적용=================
            float scrollViewWidth = myScrollRect.GetComponent<RectTransform>().rect.width; // Scrollview의 Width를 가져오기
            // 스테이지 오브젝트 positon찾기


            //float stagePosX = sta
            float Gap = 8;

            //float Gapdistance = scrollViewWidth - stagePosX / Gap;


            float distance = Vector2.Distance(posiotion, viewCenter); // 거리 계산
            float scale = Mathf.Clamp(1f, 0.1f, distance * distanceFactor); // 거리 기반 스케일 계산, 최소값 0.1f로 설정
            myScrollRect.content.GetChild(i).transform.localScale = Vector3.one * scale; // 스케일 적용
        }
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
    
    
    }
}
