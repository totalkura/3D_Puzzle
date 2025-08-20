using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapClickController : MonoBehaviour
{
    [SerializeField]
    private ScrollRect myScrollRect; // 스크롤바 컴포넌트
    public float distanceFactor = 0.1f; // 거리 조정 계수

    public void Start()
    {
        //MapValueChanged(Vector2.zero);
    }

    public void MapValueChanged(Vector2 value)
    {
        for (int i = 0; i < myScrollRect.content.childCount; i++) //childCount = 9
        {
            Vector2 posiotion = myScrollRect.content.GetChild(i).position; // Stages의 값은 W Data : -1550, H Data : 110
            Vector2 viewCenter = myScrollRect.viewport.position; // viewport의 값은 바텀 : 17

            float distance = Vector2.Distance(posiotion, viewCenter); // 거리 계산
            //float scale = 
        }
    }
}
